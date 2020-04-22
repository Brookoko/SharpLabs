namespace Store
{
    using System.Collections.Generic;
    using System.Linq;
    using Administration;

    public class AdministrationsStore : IStore
    {
        private AdministrationDatabase db = new AdministrationDatabase();

        public StorageData Units => new StorageData(db.Units);
        
        public void Add(IAdministrativeUnit unit)
        {
            if (!Contains(unit))
            {
                db.Units.Add(unit);
            }
        }
        
        public bool Contains(IAdministrativeUnit unit)
        {
            return db.Units.Contains(unit);
        }
    }
}