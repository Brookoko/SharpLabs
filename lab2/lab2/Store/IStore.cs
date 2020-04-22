namespace Store
{
    using Administration;

    public interface IStore
    {
        StorageData Units { get; }
        
        void Add(IAdministrativeUnit unit);
        
        bool Contains(IAdministrativeUnit unit);        
    }
}