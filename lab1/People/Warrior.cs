namespace Civilization.People
{
    using Nations;

    public class Warrior : Person, IBattleUnit
    {
        public float Hp { get; }
        
        public float Offence { get; }
        
        public float Defence { get; }

        public override bool IsDefensive { get; } = true;

        public Warrior(Nation nation) : base(nation)
        {
        }
    }

    public interface IBattleUnit
    {
        float Hp { get; }
        
        float Offence { get; }
        
        float Defence { get; }
    }
}