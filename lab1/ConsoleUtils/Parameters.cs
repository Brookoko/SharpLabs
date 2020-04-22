namespace Civilization
{
    using System.Collections.Generic;

    public class Parameters
    {
        public string StringParam { get; set; }
        
        public int IntParam { get; set; }
        
        public List<string> StringParameters { get; set; } = new List<string>();
    }
}