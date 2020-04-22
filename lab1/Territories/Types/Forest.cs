namespace Civilization.Territories
{
    using System.Linq;
    using People;
    using Resources;

    public class Forest : Territory<Wood>
    {
        protected override float AdditionalProductivity()
        {
            return People
                .Where(w => w is Worker)
                .Select(w => w.Productivity)
                .Sum();
        }
    }
}