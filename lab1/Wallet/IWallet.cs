namespace Civilization.Wallet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Resources;

    public interface IWallet
    {
        void Add(Resource resource);

        Resource GetAmountOf<T>() where T : IResourceType, new();

        bool CanSpent(Resource resource);

        void Spend(Resource resource);
    }
    
    public class Wallet : IWallet
    {
        private List<Account> accounts = new List<Account>();
        
        public void Add(Resource resource)
        {
            var account = GetOrCreateAccount(resource.Type);
            account.Amount += resource.Amount;
        }

        public Resource GetAmountOf<T>() where T : IResourceType, new()
        {
            var account = GetOrCreateAccount(ResourceFactory.Instance.TypeOf<T>());
            return account.Amount.Of<T>();
        }

        public bool CanSpent(Resource resource)
        {
            var account = GetOrCreateAccount(resource.Type);
            return account.Amount >= resource.Amount;
        }

        public void Spend(Resource resource)
        {
            if (!CanSpent(resource))
            {
                // error here
                return;
            }
            var account = GetOrCreateAccount(resource.Type);
            account.Amount -= resource.Amount;
        }

        public Account GetOrCreateAccount(IResourceType type)
        {
            var account = accounts.FirstOrDefault(acc => acc.Type == type);
            if (account == null)
            {
                account = new Account(type);
                accounts.Add(account);
            }
            return account;
        }
    }
}