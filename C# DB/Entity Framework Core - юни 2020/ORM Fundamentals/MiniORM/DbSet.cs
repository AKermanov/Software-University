using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MiniORM
{
    public class DbSet<TEntity> : ICollection<TEntity>
        where TEntity : class, new()
    {
        public DbSet(IEnumerable<TEntity> entities)
        {
            this.Entities = entities.ToList();
            this.ChangeTracker = new ChangeTracker<TEntity>(entities);
        }

        //запаметяваме промените
        internal ChangeTracker<TEntity> ChangeTracker {get; set;}

        //самите ентитита
        internal IList<TEntity> Entities { get; set; }

        public int Count => this.Entities.Count;

        public bool IsReadOnly => this.Entities.IsReadOnly;

        public void Add(TEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null");
            }

            this.Entities.Add(item);

            //новодобавено и трябва да се сложи в DB
            this.ChangeTracker.Add(item);
        }

        public bool Remove(TEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null");
            }

            bool removeSuccessfully = this.Entities.Remove(item);

            if (removeSuccessfully)
            {
                this.ChangeTracker.Remove(item);
            }

            return removeSuccessfully;
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities.ToArray())
            {
                this.Remove(entity);
            }
        }
        public void Clear()
        {
            while (this.Entities.Any())
            {
                TEntity entity = this.Entities.First();
                this.Remove(entity);
            }
        }

        public bool Contains(TEntity item) => this.Entities.Contains(item); 
        public void CopyTo(TEntity[] array, int startIndex)
        {
            this.Entities.CopyTo(array, startIndex);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

}