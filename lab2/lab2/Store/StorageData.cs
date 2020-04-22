namespace Store
{
    using System.Collections.Generic;
    using System.Linq;
    using Administration;

    public class StorageData
    {        
        private IEnumerable<IAdministrativeUnit> Data { get; set; }
        public List<IAdministrativeUnit> List => Data.ToList();

        public StorageData(IEnumerable<IAdministrativeUnit> data)
        {
            Data = data;
        }
        
        public StorageData Of(AdministrativeType type)
        {
            Data = Data.Where(unit => (unit.Type & type) != 0);
            return this;
        }

        public IAdministrativeUnit FindByName(string name)
        {
            return Data.FirstOrDefault(unit => unit.Name == name);
        }
    }
}