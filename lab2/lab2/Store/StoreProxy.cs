namespace Store
{
    using System.Collections.Generic;
    using Administration;

    public class StoreProxy : IStore
    {
        private IStore store;
        private List<IAdministrativeUnit> units = new List<IAdministrativeUnit>();

        public StorageData Units
        {
            get
            {
                var storedUnits = store.Units;
                SynchronizeUnits(storedUnits.List);
                return storedUnits;
            }
        }

        public StoreProxy(IStore store)
        {
            this.store = store;
        }
        
        public void Add(IAdministrativeUnit unit)
        {
            if (!units.Contains(unit))
            {
                units.Add(unit);
                store.Add(unit);
            }
        }
        
        public bool Contains(IAdministrativeUnit unit)
        {
            return units.Contains(unit) || store.Contains(unit);
        }
        
        private void SynchronizeUnits(List<IAdministrativeUnit> storedUnits)
        {
            units = storedUnits;
        }
    }
}