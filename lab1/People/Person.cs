namespace Civilization.People
{
    using Nations;

    public abstract class Person
    {
        public Nation Nation { get; }
        
        public virtual bool IsDefensive { get; }
        
        public float Productivity { get; set; }
        
        public string Id { get; set; }
        
        public Person(Nation nation)
        {
            Nation = nation;
        }
    }
}