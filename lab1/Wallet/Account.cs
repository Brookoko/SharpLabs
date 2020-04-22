namespace Civilization.Wallet
{
    using Resources;

    public class Account
    {
        public IResourceType Type { get; }
        
        public float Amount { get; set; }
        
        public Account(IResourceType type)
        {
            Type = type;
        }
    }
}