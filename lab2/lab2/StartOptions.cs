namespace ConsoleUtils.Types
{
    using System;
    using Administration;
    using Store;

    public class StartOptions : Options
    {
        public override string Id => "Start";

        private IStore store;
        private AdministrationFactory factory = new AdministrationFactory();
        
        public StartOptions(IStore store)
        {
            this.store = store;
            AddOption("--create $type$ $name$", parameters => CreateUnit(parameters.Strings[0], parameters.Strings[1]));
            AddOption("--find $name$", parameters => FindUnit(parameters.String));
            AddOption("--subunit $parent$ $child$", parameters => SetAsSubunit(parameters.Strings[0], parameters.Strings[1]));
            AddOption("--subunitType $parent$ $parentType$ $child$ $childType$",
                parameters => SetAsSubunit(parameters.Strings[0], parameters.Strings[1], parameters.Strings[2],
                    parameters.Strings[3]));
            AddOption("--list", parameters => PrintUnits());
        }
        
        private void CreateUnit(string type, string name)
        {
            var unit = factory.CreateFromType(type, name);
            if (unit != null)
            {
                store.Add(unit);
                Console.WriteLine($"Create new unit of type {type} with name {name}\n");
                return;
            }
            Console.WriteLine($"Cannot create unit of type {type}\n");
        }
        
        private void FindUnit(string name)
        {
            var unit = store.Units.FindByName(name);
            if (unit == null)
            {
                Console.WriteLine($"No unit with name {name}\n");
            }
            else
            {
                Console.WriteLine($"Find unit with name {name}\n");
            }
        }
        
        private void SetAsSubunit(string parentName, string childName)
        {
            SetAsSubunit(parentName, ~AdministrativeType.None, childName, ~AdministrativeType.None);
        }
        
        private void SetAsSubunit(string parentName, string parentType, string childName, string childType)
        {
            if (Enum.TryParse<AdministrativeType>(parentType, true, out var parent))
            {
                if (Enum.TryParse<AdministrativeType>(childType, true, out var child))
                {
                    SetAsSubunit(parentName, parent, childName, child);
                    return;
                }
                Console.WriteLine($"No type with name {childType}");
                return;
            }
            Console.WriteLine($"No type with name {parentType}");
        }
        
        private void SetAsSubunit(string parentName, AdministrativeType parentType, string childName, AdministrativeType childType)
        {
            var parent = store.Units.Of(parentType).FindByName(parentName);
            if (parent == null)
            {
                Console.WriteLine($"No unit with name {parentName}\n");
                return;
            }

            var child = store.Units.Of(childType).FindByName(childName);
            if (child == null)
            {
                Console.WriteLine($"No unit with name {childName}\n");
                return;
            }
            
            if (parent.CanAddAsSubunit(child))
            {
                parent.AddSubunit(child);
                Console.WriteLine($"Add subunit {childName} to {parentName}\n");
                return;
            }
            Console.WriteLine($"Cannot add {childName} as subunit of {parentName}\n");
            Console.WriteLine("If you have unit with same name try to specify type");
        }
        
        private void PrintUnits()
        {
            foreach (var unit in store.Units.List)
            {
                Console.WriteLine($"{unit.Name} ({unit.Type})");
            }
        }
    }
}