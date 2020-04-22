namespace Store
{
    using System.Collections.Generic;
    using Administration;

    public class AdministrationDatabase
    {
        public List<IAdministrativeUnit> Units { get; } = new List<IAdministrativeUnit>();
    }
}