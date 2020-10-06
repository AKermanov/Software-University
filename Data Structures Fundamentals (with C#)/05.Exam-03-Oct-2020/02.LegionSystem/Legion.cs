namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _02.LegionSystem.Interfaces;
    using Wintellect.PowerCollections;

    public class Legion : IArmy
    {
        private OrderedBag<IEnemy> enemies;

        public Legion()
        {
            this.enemies = new OrderedBag<IEnemy>();
        }
        public int Size => this.enemies.Count;

        public bool Contains(IEnemy enemy)
        {
            return enemies.Contains(enemy);
        }

        public void Create(IEnemy enemy)
        {
           
            if (!enemies.Contains(enemy))
            {
                this.enemies.Add(enemy);
            }

            /*
            bool isExist = true;
            foreach (var currEnemy in enemies)
            {
                if (currEnemy.AttackSpeed == enemy.AttackSpeed)
                {
                    isExist = false;
                    break;
                }
            }
            if (isExist)
            {
                this.enemies.Add(enemy);
            }
            */
        }

        //worked
        public IEnemy GetByAttackSpeed(int speed)
        {
            IEnemy curr = null;
            foreach (var enemy in enemies)
            {
                if (enemy.AttackSpeed == speed)
                {
                    curr = enemy;
                }
            }
            return curr;

           // return enemies.FirstOrDefault(x => x.AttackSpeed == speed);
        }

        public List<IEnemy> GetFaster(int speed)
        {
            var list = new List<IEnemy>();
            foreach (var current in enemies)
            {
                if (current.AttackSpeed > speed)
                {
                    list.Add(current);
                }
            }

            return list;
            //return enemies.Where(x => x.AttackSpeed > speed).ToList();
        }

        public IEnemy GetFastest()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }

            var curr = enemies[0];
            return curr;
        }

        public IEnemy[] GetOrderedByHealth()
        {
            var curr = enemies.OrderByDescending(x => x.Health).ToArray();
                        return curr;
        }

        public List<IEnemy> GetSlower(int speed)
        {
            var list = new List<IEnemy>();
            foreach (var current in enemies)
            {
                if (current.AttackSpeed < speed)
                {
                    list.Add(current);
                }
            }

            return list;
            //return enemies.Where(x => x.AttackSpeed < speed).ToList();
        }

        public IEnemy GetSlowest()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }

            var curr = enemies[Size - 1];
            return curr;
        }

        public void ShootFastest()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }

            var curr = enemies[0];
            enemies.Remove(curr);
        }

        public void ShootSlowest()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }

            var curr = enemies[Size - 1];
            enemies.Remove(curr);
        }
    }
}
