namespace Civilization.Nations
{
    using System;
    using System.Collections.Generic;
    using Territories;
    using People;
    using Wallet;

    public class Nation
    {
        public string Name { get; }
        
        public List<Person> Population { get; } = new List<Person>();
        
        public List<Territory> Territories { get; } = new List<Territory>();

        public DateTime LastResourceCollection { get; set; } = DateTime.UtcNow;
        
        public IWallet Wallet { get; } = new Wallet();
        
        public Nation(string name)
        {
            Name = name;
        }
    }
}