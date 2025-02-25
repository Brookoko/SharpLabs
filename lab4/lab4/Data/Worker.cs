namespace Data
{
    public class Worker
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string FullName => $"{FirstName} {SecondName}";
        
        public string Role { get; set; }

        public override string ToString()
        {
            return $"{FullName} ({Role})";
        }
    }
}