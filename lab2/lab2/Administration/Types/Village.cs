namespace Administration
{
    public class Village : AdministrativeUnit
    {
        public override AdministrativeType Type => AdministrativeType.Village | AdministrativeType.Locality;

        public override AdministrativeType CanWorkWith => AdministrativeType.None;

        public Village(string name) : base(name)
        {
        }        
    }
}