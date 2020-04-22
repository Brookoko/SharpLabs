namespace Civilization.Resources
{
    public struct Resource
    {
        public float Amount { get; }
        
        public IResourceType Type { get; }
        
        public Resource(IResourceType type, float amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}