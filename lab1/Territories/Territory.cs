namespace Civilization.Territories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Nations;
    using Resources;
    using People;

    public abstract class Territory : IResourceProducer
    {
        public string Id { get; set; }
        
        public Nation OwnedBy { get; private set; }

        public List<Person> People { get; } = new List<Person>();
        
        public float ProductivityPerSecond { get; set; }

        public abstract Resource CollectResourceFor(TimeSpan span);

        public bool TryAcquire(Nation nation)
        {
            if (CanBeAcquireBy(nation))
            {
                nation?.Territories.Remove(this);
                OwnedBy = nation;
                People.Clear();
                OwnedBy.Territories.Add(this);
                return true;
            }
            return false;
        }

        private bool CanBeAcquireBy(Nation nation)
        {
            if (nation == null) return false;
            if (People == null || People.Count == 0) return true;
            var defensive = People.FirstOrDefault(p => p.IsDefensive);
            return defensive == null || defensive.Nation == nation;
        }

        public void AddPeople(IEnumerable<Person> people)
        {
            people = people.Where(p => p.Nation == OwnedBy && !People.Contains(p));
            People.AddRange(people);
        }
    }
    
    public abstract class Territory<T> : Territory where T : ResourceType<T>, new()
    {
        public override Resource CollectResourceFor(TimeSpan span)
        {
            return AmountFor(span).Of<T>();
        }
        
        private float AmountFor(TimeSpan span)
        {
            var additionalProductivity = AdditionalProductivity();
            return (float) span.TotalSeconds * (ProductivityPerSecond + additionalProductivity);
        }

        protected abstract float AdditionalProductivity();
    }
}