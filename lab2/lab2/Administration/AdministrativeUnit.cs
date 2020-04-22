namespace Administration
{
    using System.Collections.Generic;

    public abstract class AdministrativeUnit : IAdministrativeUnit
    {
        public abstract AdministrativeType Type { get; }
        
        public abstract AdministrativeType CanWorkWith { get; }
        
        public IEnumerable<IAdministrativeUnit> Subunits => subunits;
        private readonly List<IAdministrativeUnit> subunits = new List<IAdministrativeUnit>();
        
        public string Name { get; }

        protected AdministrativeUnit(string name)
        {
            Name = name;
        }

        public bool CanAddAsSubunit(IAdministrativeUnit unit)
        {
            return (CanWorkWith & unit.Type) != 0;
        }
        
        public void AddSubunit(IAdministrativeUnit unit)
        {
            if (CanAddAsSubunit(unit))
            {
                subunits.Add(unit);
            }
        }
    }
}