namespace Data
{
    using System;
    using System.Collections.Generic;

    public class Project
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public float Cost { get; set; }
        
        public DateTime Start { get; set; }
        
        public DateTime End { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public List<Worker> Workers { get; set; }
    }
}