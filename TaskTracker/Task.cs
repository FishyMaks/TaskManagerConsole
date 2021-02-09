using System;
using System.Collections.Generic;
using System.Runtime.Serialization; // Needed to make objects serializable (ISerializable)

namespace TaskTracker
{
    public class Task : ISerializable
    {
        public string Name { get; set; }
        public List<Employee> WorkersAssigned { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }
        public bool Complete { get; set; }

        Task() { } // Parameterless constructor is required for XML serialization

        public Task(string name, List<Employee> workersAssigned = null)
        {
            Name = name.ToLower().Trim();
            WorkersAssigned = workersAssigned;
            DateCreated = DateTime.Now;
            DateCompleted = null;
            Complete = false;
        }

        // Part of the ISerializable interface needed to serialize (save) classes
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("WorkersAssigned", WorkersAssigned);
            info.AddValue("DateCreated", DateCreated);
            info.AddValue("DateCompleted", DateCompleted);
            info.AddValue("Complete", Complete);
        }

        // Overrides the default ToString behavior, provides string representation of Task
        public override string ToString()
        {
            return $"Task: {Name} | Created {DateCreated} | {(Complete ? "Complete" : "Incomplete")}";
        }
    }
}