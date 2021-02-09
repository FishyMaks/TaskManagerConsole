using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using static TaskTracker.Menu;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTracker
{
    class Program
    {


        public static List<Task> Tasks { get; set; } = new List<Task>();
        public static List<Employee> Employees { get; set; } = new List<Employee>();
        static XmlSerializer SerializerTask = new XmlSerializer(typeof(List<Task>));
        static XmlSerializer SerializerEmp = new XmlSerializer(typeof(List<Employee>));

        static void Main(string[] args)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            LoadTasks();
            LoadEmployee();
            ShowMainMenu();
        }


        public static void SaveTasks(List<Task> tasks)
        {
            // Serialize Tasks to file stream
            using Stream stream = File.Open("./tasks.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            SerializerTask.Serialize(stream, tasks);
        }

        public static void LoadTasks()
        {
            try
            {
                // Open file stream and deserialize to Tasks
                using Stream stream = File.OpenRead("./tasks.xml");
                Tasks = (List<Task>)SerializerTask.Deserialize(stream);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("A local tasks file was not found. Generating new file...");
                Thread.Sleep(2500);
            }


        }
        public static void SaveEmployee(List<Employee> employee)
        {
            // Serialize Tasks to file stream
            using Stream stream = File.Open("./employee.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            SerializerEmp.Serialize(stream, employee);
        }

        public static void LoadEmployee()
        {
            try
            {
                // Open file stream and deserialize to Tasks
                using Stream stream = File.OpenRead("./employee.xml");
                Employees = (List<Employee>)SerializerEmp.Deserialize(stream);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("A local employee file was not found. Generating new file...");
                Thread.Sleep(2500);
            }
        }
    }
}
