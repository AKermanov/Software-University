using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniORM
{
	internal class ChangeTracker<T>
    where T : class, new() //добавяме констраин, къде Т да е клас(refference) и да има конструктор
    {
        private readonly List<T> allEntities;

        private readonly List<T> added;

        private readonly List<T> removed;

        public ChangeTracker(IEnumerable<T> entities)
        {
            this.added = new List<T>();
            this.removed = new List<T>();

            this.allEntities = CloneEntities(entities);
        }

        //с това пропърти достъпваме колекциите
        public IReadOnlyCollection<T> AllEntities => this.allEntities.AsReadOnly();
        public IReadOnlyCollection<T> Added => this.added.AsReadOnly();
        public IReadOnlyCollection<T> Removed => this.removed.AsReadOnly();

        public void Add(T item) => this.added.Add(item);

        //change tracker, казваме "ти си за изтриване"
        public void Remove(T item) => this.removed.Add(item);

        public IEnumerable<T> GetModifiedEntities(DbSet<T> dbSet)
        {
            List<T> modifiedEntities = new List<T>();

            PropertyInfo[] primaryKeys = typeof(T)
                .GetProperties()
                .Where(pi => pi.HasAttribute<KeyAttribute>())
                .ToArray();

            //Какво правят атрибутуте в c#?
            //дават метаданни

            foreach (T proxyEntity in this.AllEntities)
            {
                //мачва ентитио от базата, което е равно на нашето прокси ентити
                object[] primaryKeyValues = GetPrimaryKeyValues(primaryKeys, proxyEntity).ToArray();

                //single връща 1ят отговарящ, който е единствен
                T entity = dbSet.Entities.Single(e => GetPrimaryKeyValues(primaryKeys, e).SequenceEqual(primaryKeys));

                bool isModified = IsModified(proxyEntity, entity);

                if (isModified)
                {
                    modifiedEntities.Add(entity);
                }
            }

            return modifiedEntities;
        }

        private static IEnumerable<object> GetPrimaryKeyValues(IEnumerable<PropertyInfo> primaryKeys, T proxyEntity)
        {
            //за всичким които имаме пропърти да върне валюто
            return primaryKeys.Select(pi => pi.GetValue(proxyEntity));
        }
        private static bool IsModified(T proxyEntity, T entity)
        {
            //взима всички пропъртита, които са съвместими с SQL типовете
            PropertyInfo[] monitoredProperties = typeof(T)
                .GetProperties()
                .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType))
                .ToArray();

            //дай ми всички пропъртита, където value на едното proxyEntity не начва value на другото realEntity
            PropertyInfo[] modifiedProperties = monitoredProperties
                .Where(pi => !Equals(pi.GetValue(entity), pi.GetValue(proxyEntity)))
                .ToArray();

            return modifiedProperties.Any();
        }

        private static List<T> CloneEntities(IEnumerable<T> entities)
        {
            List<T> clonedEntities = new List<T>();

            PropertyInfo[] propertiesToClone = typeof(T)
                .GetProperties()
                .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType)).ToArray();

            //истинското ентити
            foreach (T entity in entities)
            {//клонираното ентити
                T clone = Activator.CreateInstance<T>();

                //във всяко пропърти, което е клонирано му взимаме истинското ентити
                foreach (PropertyInfo property in propertiesToClone)
                {
                    //с рефлекшън слагаме пропъртитата едно в друго
                    object value = property
                        .GetValue(entity);
                    property.SetValue(clone, value);
                }

                clonedEntities.Add(clone);
            }

            return clonedEntities;
        }
    }
}