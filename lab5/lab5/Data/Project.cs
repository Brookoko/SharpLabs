namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    [XmlType("Project")]
    public class Project
    {
        [XmlElement]
        public int Id { get; set; }
        
        [XmlElement]
        public string Name { get; set; }
        
        [XmlElement]
        public float Cost { get; set; }
        
        [XmlElement]
        public DateTime Start { get; set; }
        
        [XmlElement]
        public DateTime End { get; set; }
        
        [XmlElement]
        public bool IsCompleted { get; set; }
        
        [XmlElement]
        public List<Worker> Workers { get; set; }
        
        public override string ToString()
        {
            var workers = Workers.Aggregate("", (acc, w) => acc + " : " + w).Substring(3);
            return $"{Name}  {Start:MM/dd/yyyy}-{(IsCompleted ? End.ToString(@"MM/dd/yyyy") : "")} ({Cost:F2}) {workers}";
        }
    }
}