using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace MiniORM
{
    public abstract class DbContext
    {
        private readonly DatabaseConnection connection;

        private readonly Dictionary<Type, PropertyInfo> dbSetProperties;

        protected DbContext(string connectionString)
        {
            this.connection = new DatabaseConnection(connectionString);

            this.dbSetProperties = this.DiscoverDbSets();

            using (new ConnectionManager(this.connection))
            {
                this.InitializeDbSets();
            }

            //navigation properties
            this.MapAllRelations();
        }

        //типовете данни, които могат да се ползват в таблицитр
        internal static readonly Type[] AllowedSqlTypes =
        {
          typeof(string),
          typeof(int),
          typeof(uint),
          typeof(long),
          typeof(ulong),
          typeof(decimal),
          typeof(bool),
          typeof(DateTime)
        };

        public void SaveChanges()
        {
            object[] dbSets = this.dbSetProperties
                .Select(pi => pi.Value.GetValue(this)) //взимаме за сегашния контест
                .ToArray();

            foreach (IEnumerable<object> dbSet in dbSets)
            {
                object[] invalidEntities = dbSet.
                    Where(entity => !IsObjectValid(entity))
                    .ToArray();

                if (invalidEntities.Any())
                {
                    throw new InvalidOperationException($"{invalidEntities.Length} Invalid Entities found in {dbSet.GetType().Name}!");
                }
            }
            using (new ConnectionManager(this.connection))
            {
                using (SqlTransaction transaction = this.connection.StartTransaction())
                {
                    foreach (IEnumerable dbSet in dbSets)
                    {
                        Type dbSetType = dbSet.GetType()
                            .GetGenericArguments()
                            .First();


                        //даваме критерии по които да търсим
                        MethodInfo persistMethod = typeof(DbContext)
                            .GetMethod("Persist", BindingFlags.Instance | BindingFlags.NonPublic)
                            .MakeGenericMethod(dbSetType);

                        try
                        {
                            //извикваме метода 
                            persistMethod.Invoke(this, new object[] { dbSet });
                        }
                        catch (TargetInvocationException tie)
                        {
                            throw tie.InnerException;
                        }
                        catch (InvalidOperationException)
                        {
                            transaction.Rollback();
                            throw;
                        }
                        catch (SqlException)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                    transaction.Commit();
                }
            }
        }

        private void Persist<TEntity>(DbSet<TEntity> dbSet)
            where TEntity : class, new()
        {
            string tableName = this.GetTableName(typeof(TEntity));

            string[] columns = this.connection.FetchColumnNames(tableName).ToArray();

            if (dbSet.ChangeTracker.Added.Any())
            {
                this.connection.InsertEntities(dbSet.ChangeTracker.Added, tableName, columns);
            }

            TEntity[] modifiedEntities = dbSet
                .ChangeTracker.GetModifiedEntities(dbSet).ToArray();

            if (modifiedEntities.Any())
            {
                this.connection.UpdateEntities(modifiedEntities, tableName, columns);
            }

            if (dbSet.ChangeTracker.Removed.Any())
            {
                this.connection.DeleteEntities(dbSet.ChangeTracker.Removed, tableName, columns);
            }
        }

        private void InitializeDbSets()
        {
            foreach (var dbSet in this.dbSetProperties)
            {
                Type dbSetType = dbSet.Key;
                PropertyInfo dbSetProperty = dbSet.Value;

                MethodInfo populateDbSetGeneric = typeof(DbContext)
                    .GetMethod("PopulateDbSet", BindingFlags.Instance | BindingFlags.NonPublic)
                    .MakeGenericMethod(dbSetType);

                populateDbSetGeneric.Invoke(this, new object[] { dbSetProperty });
            }
        }

        private void PopulateDbSet<TEntity>(PropertyInfo dbSet)
            where TEntity : class, new()
        {
            IEnumerable<TEntity> entities = this.LoadTableEntities<TEntity>();

            DbSet<TEntity> dbSetInstance = new DbSet<TEntity>(entities);

            //this - db set, тук ги сетваме, понеже нямат сетари
            ReflectionHelper.ReplaceBackingField(this, dbSet.Name, dbSetInstance);
        }

        private void MapAllRelations()
        {
            foreach (var dbSetProperty in this.dbSetProperties)
            {
                Type dbSetType = dbSetProperty.Key;

                object dbSet = dbSetProperty.Value.GetValue(this);

                MethodInfo mapRelationGeneric = typeof(DbContext)
                    .GetMethod("MapRelations", BindingFlags.Instance | BindingFlags.NonPublic)
                    .MakeGenericMethod(dbSetType);

                mapRelationGeneric.Invoke(this, new object[] { dbSet });
            }
        }

        private void MapRelations<TEntity>(DbSet<TEntity> dbSet)
            where TEntity : class, new()
        {
            //на ентитито, което ни се подава му взимаме ентитъ тайп
            Type entityType = typeof(TEntity);

            MapNavigationProperties(dbSet);

            //взимаме всички пропертита, които са генерик тайп
            //generictypedefinition им е iCollection
            PropertyInfo[] collections = entityType
                .GetProperties()
                .Where(pi =>
                pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                .ToArray();

            foreach (PropertyInfo collection in collections)
            {
                Type collectionType = collection //влиза в колеквията
                    .PropertyType                //вижда какъв е property type
                    .GenericTypeArguments        //взима generic
                    .First();                    //и дава първият, понеже е само един

                MethodInfo mapCollectionMethod = typeof(DbContext)
                    .GetMethod("MapCollection", BindingFlags.Instance | BindingFlags.NonPublic)
                    .MakeGenericMethod(entityType, collectionType);

                //викаме метода
                mapCollectionMethod.Invoke(this, new object[] { dbSet, collection });
            }

        }

        private void MapCollection<TDbSet, TCollection>(DbSet<TDbSet> dbSets, PropertyInfo collectionPropery)
            where TDbSet : class, new() 
            where TCollection : class, new()
        {
            Type entityType = typeof(TDbSet);
            Type collectionType = typeof(TCollection);

            PropertyInfo[] primaryKeys = collectionType
                .GetProperties()
                .Where(pi => pi.HasAttribute<KeyAttribute>())
                .ToArray();

            PropertyInfo primaryKey = primaryKeys.First();
            PropertyInfo foreignKey = entityType
                .GetProperties()
                .First(pi => pi.HasAttribute<KeyAttribute>());

            bool isManyToMany = primaryKeys.Length >= 2;

            if (isManyToMany)
            {
                primaryKey = collectionType
                    .GetProperties()
                    .First(pi => collectionType
                                .GetProperty(pi.GetCustomAttribute<ForeignKeyAttribute>().Name)
                    .PropertyType == entityType);
            }

            DbSet<TCollection> navigationDbSet = (DbSet<TCollection>)
                this.dbSetProperties[collectionType]
                .GetValue(this);

            foreach (TDbSet entity in dbSets)
            {
                object primaryKeyValue = foreignKey.GetValue(entity);

                TCollection[] navigationEntities = navigationDbSet
                     .Where(ne => primaryKey.GetValue(ne).Equals(primaryKeyValue))
                     .ToArray();

                ReflectionHelper.ReplaceBackingField(entity, collectionPropery.Name, navigationEntities);
            }
        }

        private void MapNavigationProperties<TEntity>(DbSet<TEntity> dbSet)
            where TEntity: class, new()
        {
            Type entityType = typeof(TEntity);

            PropertyInfo[] foreignKeys = entityType
                .GetProperties()
                .Where(pi => pi.HasAttribute<ForeignKeyAttribute>())
                .ToArray();

            foreach (PropertyInfo foreignKey in foreignKeys)
            {
                string navigationPropertyName = foreignKey
                    .GetCustomAttribute<ForeignKeyAttribute>().Name;

                PropertyInfo navigationProperty = entityType
                    .GetProperty(navigationPropertyName);

                object navigationDbSet = this.dbSetProperties[navigationProperty.PropertyType]
                    .GetValue(this);

                PropertyInfo navigationPrimaryKey = navigationProperty.PropertyType
                    .GetProperties()
                    .First(pi => pi.HasAttribute<KeyAttribute>());

                foreach (TEntity entity in dbSet)
                {
                    object foreignKeyValue = foreignKey.GetValue(entity);
                    object navigationPropertyValues =((IEnumerable<object>) navigationDbSet)
                        .First(cnp => navigationPrimaryKey.GetValue(cnp).Equals(foreignKeyValue));

                    navigationProperty.SetValue(entity, navigationPropertyValues);
                }

            }
        }

        private static bool IsObjectValid(object e)
        {
            ValidationContext validetionContext = new ValidationContext(e);
            List<ValidationResult> validationError = new List<ValidationResult>();

            bool validationResult = Validator
                .TryValidateObject(e, validetionContext, validationError, true);

            return validationResult;
        }

        private IEnumerable<TEntity> LoadTableEntities<TEntity>()
            where TEntity : class
        {
            Type table = typeof(TEntity);

            string[] columns = GetEntityColumnNames(table);

            string tableName = GetTableName(table);

            TEntity[] fethcRows = this.connection
                .FetchResultSet<TEntity>(tableName, columns)
                .ToArray();

            return fethcRows;
        }

        private string GetTableName(Type tableType)
        {
            string tableName = ((TableAttribute)Attribute.GetCustomAttribute(tableType,typeof(TableAttribute))).Name;

            if (tableName == null)
            {
                tableName = this.dbSetProperties[tableType].Name;
            }

            return tableName;
        }

        private Dictionary<Type, PropertyInfo> DiscoverDbSets()
        {
            Dictionary<Type, PropertyInfo> dbSet = this.GetType()
                .GetProperties()
                .Where(pi => pi.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .ToDictionary(pi => pi.PropertyType.GetGenericArguments().First(), pi => pi);

            return dbSet;
        }

        private string[] GetEntityColumnNames(Type table)
        {
            string tableName = GetTableName(table);
            IEnumerable<string> dbColumns = this.connection
                .FetchColumnNames(tableName);

            string[] columns = table.GetProperties()
                .Where(pi => dbColumns.Contains(pi.Name) &&
                            !pi.HasAttribute<NotMappedAttribute>() &&
                             AllowedSqlTypes.Contains(pi.PropertyType))
                .Select(pi => pi.Name)
                .ToArray();

            return columns;
        }
    }
}