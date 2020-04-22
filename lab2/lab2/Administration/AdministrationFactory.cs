namespace Administration
{
    using System;
    using System.Collections.Generic;

    public class AdministrationFactory
    {
        private static readonly Dictionary<string, Type> TypeMap = new Dictionary<string, Type>
        {
            {"country", typeof(Country)},
            {"region", typeof(Region)},
            {"city", typeof(City)},
            {"village", typeof(Village)},
            {"district", typeof(District)}
        };

        public IAdministrativeUnit CreateFromType(string type, string name)
        {
            if (TypeMap.TryGetValue(type, out var t))
            {
                var constructor = t.GetConstructor(new[] {typeof(string)});
                return (IAdministrativeUnit) constructor.Invoke(new object[] {name});
            }
            return null;
        }
    }
}