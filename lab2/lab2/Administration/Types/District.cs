namespace Administration
{
    public class District : AdministrativeUnit
    {
        public override AdministrativeType Type => AdministrativeType.District;

        public override AdministrativeType CanWorkWith => AdministrativeType.None;

        public District(string name) : base(name)
        {
        }
    }
}