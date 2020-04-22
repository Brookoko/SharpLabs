namespace Administration
{
    public class Country : AdministrativeUnit
    {
        public override AdministrativeType Type => AdministrativeType.Country;

        public override AdministrativeType CanWorkWith => AdministrativeType.Region;

        public Country(string name) : base(name)
        {
        }
    }
}