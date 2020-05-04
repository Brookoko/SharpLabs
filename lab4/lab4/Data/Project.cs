namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Project
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public float Cost { get; set; }
        
        public DateTime Start { get; set; }
        
        public DateTime End { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public List<Worker> Workers { get; set; }
        
        public override string ToString()
        {
            var workers = Workers.Aggregate("", (acc, w) => acc + " : " + w).Substring(3);
            return $"{Name}  {Start:MM/dd/yyyy}-{(IsCompleted ? End.ToString(@"MM/dd/yyyy") : "")} ({Cost:F2}) {workers}";
        }
    }
}