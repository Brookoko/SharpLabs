namespace Administration
{
    using System;

    [Flags]
    public enum AdministrativeType
    {
        None = 1,
        District = 2,
        Village = 4,
        City = 8,
        Locality = 16,
        Region = 32,
        Country = 64
    }
}