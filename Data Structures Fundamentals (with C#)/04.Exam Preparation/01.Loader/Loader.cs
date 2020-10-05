namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Loader : IBuffer
    {
        private List<IEntity> entities;

        public Loader()
        {
            this.entities = new List<IEntity>();
        }
        public int EntitiesCount => this.entities.Count;

        public void Add(IEntity entity)
        {
            this.entities.Add(entity);
        }

        public void Clear()
        {
            this.entities.Clear();
        }

        public bool Contains(IEntity entity)
        {
            return this.GetById(entity.Id) != null;
        }

        public IEntity Extract(int id)
        {
            IEntity found = this.GetById(id);

            if (found != null)
            {
                this.entities.Remove(found);
            }
            return found;
        }

        public IEntity Find(IEntity entity)
        {
            return this.GetById(entity.Id);
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this.entities);
        }

      

        public void RemoveSold()
        {
            this.entities.RemoveAll(e => e.Status == BaseEntityStatus.Sold);
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            int indexOfEntity = this.entities.IndexOf(oldEntity);
            this.ValidateEntity(indexOfEntity);

            this.entities[indexOfEntity] = newEntity;

        }

        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            var result = new List<IEntity>(this.EntitiesCount);
            int lowerBoundIndex = (int)lowerBound;
            int upperBoundIndex = (int)upperBound;

            for (int i = 0; i < this.EntitiesCount; i++)
            {
                var entity = this.entities[i];
                int entityStatusIndex = (int)entity.Status;

                if (entityStatusIndex >= lowerBoundIndex && 
                    entityStatusIndex <= upperBoundIndex)
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public void Swap(IEntity first, IEntity second)
        {
            int indexOfFIrst = this.entities.IndexOf(first);
            int indexOfSecond = this.entities.IndexOf(second);
            this.ValidateEntity(indexOfFIrst);
            this.ValidateEntity(indexOfSecond);

            var temp = this.entities[indexOfFIrst];
            this.entities[indexOfFIrst] = this.entities[indexOfSecond];
            this.entities[indexOfSecond] = temp;
        }

        public IEntity[] ToArray()
        {
            return this.entities.ToArray();
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            for (int i = 0; i < this.EntitiesCount; i++)
            {
                var current = this.entities[i];

                if (current.Status == oldStatus)
                {
                    current.Status = newStatus;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public IEnumerator<IEntity> GetEnumerator()
        {
            return this.entities.GetEnumerator();
        }
        private IEntity GetById(int id)
        {
            for (int i = 0; i < this.EntitiesCount; i++)
            {
                var currentEntity = this.entities[i];

                if (currentEntity.Id == id)
                {
                    return currentEntity;
                }
            }

            return null;
        }

        private void ValidateEntity(int index)
        {
            if (index == -1)
            {
                throw new InvalidOperationException("Entity not found");
            }
        }

      
    }
}
