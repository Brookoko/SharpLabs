namespace Administration
{
    public class Region : AdministrativeUnit
    {
        public override AdministrativeType Type => AdministrativeType.Region;

        public override AdministrativeType CanWorkWith => AdministrativeType.Locality;

        public Region(string name) : base(name)
        {
        }        
    }
}