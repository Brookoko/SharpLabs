namespace Administration
{
    using System.Collections.Generic;

    public interface IAdministrativeUnit
    {
        string Name { get; }
        
        AdministrativeType Type { get; }

        IEnumerable<IAdministrativeUnit> Subunits { get; }
        
        bool CanAddAsSubunit(IAdministrativeUnit unit);
        
        void AddSubunit(IAdministrativeUnit unit);
    }
}