namespace Administration
{
    public class City : AdministrativeUnit
    {
        public override AdministrativeType Type => AdministrativeType.City | AdministrativeType.Locality;
       
        public override AdministrativeType CanWorkWith => AdministrativeType.District;
        
        public City(string name) : base(name)
        {
        }
    }
}