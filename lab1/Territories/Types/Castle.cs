namespace Civilization.Territories
{
    using System.Linq;
    using People;
    using Resources;

    public class Castle : Territory<Gold>
    {
        protected override float AdditionalProductivity()
        {
            return People
                .Where(n => n is Noble)
                .Select(n => n.Productivity)
                .Sum();
        }
    }
}