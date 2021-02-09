using System;
using static TaskTracker.Program;
using static TaskTracker.Task;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace TaskTracker
{
    public static class Menu
    {
        public static int Check(string input)
        {
            int output;
            while(!int.TryParse(input, out output))
            {
                Console.Write("Please enter a valid input: ");
                input = Console.ReadLine();
            }return output;
        }

        public static void ShowMainMenu()
        {
            //is public the correct access modifier
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=============== Main Menu ==============");
                Console.WriteLine("1. Tasks");
                Console.WriteLine("2. Employees");
                Console.WriteLine("3. Quit");
                Console.WriteLine("========================================");


                int userInput = Check(Console.ReadLine()); //tryparse didnt work? use the out reference?
                Console.Clear();

                switch (userInput)  //Look up using enums here, so the user can type anser instead of number
                {
                    case 1:
                        //user selected tasks
                        ShowTaskMenu();
                        break;
                    case 2:
                        //user selected employees
                        ShowEmployeeMenu();
                        break;
                    case 3:
                        //user selected quit, close the program
                        Console.WriteLine("exiting....");
                        Thread.Sleep(4000);
                        Environment.Exit(0);
                        break;
                    default:
                        //user did not enter a valid selection, reload menu
                        Console.WriteLine("invalid user input, please try again");
                        break;
                }
                Console.Clear();
            }
        }


        public static void ShowTaskMenu()
        {
            Console.WriteLine("=============== Task Menu ==============");
            Console.WriteLine("1. Show Active Tasks");
            Console.WriteLine("2. Show Completed Tasks");
            Console.WriteLine("3. Create Task");
            Console.WriteLine("4. Return to Main Menu");
            Console.WriteLine("========================================");
            int userInput = Check(Console.ReadLine());
            switch (userInput)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("1. Show Active Tasks");
                    Console.WriteLine("========================================");
                    LoadTasks();
                    if (Tasks.Any())
                    {
                        for (int i = 0; i < Tasks.Count; i++)
                        {
                            if (!Tasks[i].Complete)
                            {
                                Console.Write($"{Tasks[i]?.ToString()}. Is this task complete, Yes/No?");

                                string input = Console.ReadLine().ToLower().Trim();
                                while (input != "yes" && input != "no")
                                {
                                    Console.Write("That was not a valid entry: ");
                                    input = Console.ReadLine().ToLower().Trim();
                                }
                                if (input == "yes") { Tasks[i].Complete = true; SaveTasks(Tasks); return; }

                                Console.Write($"Do you want to add a worker to this task, Yes/No?");

                                string input2 = Console.ReadLine().ToLower().Trim();
                                while (input2 != "yes" && input2 != "no")
                                {
                                    Console.Write("That was not a valid entry: ");
                                    input2 = Console.ReadLine().ToLower().Trim();
                                }
                                if (input2 == "yes")
                                {
                                    Console.Write("Enter an employee name: ");
                                    string input3 = Console.ReadLine().ToLower().Trim();
                                    bool check = true;
                                    for (int e = 0; e < Employees.Count; e++)
                                    {
                                        if (Employees[e].Name == input3)
                                        {
                                            Employee emp = new Employee(Employees[e].Name, Employees[e].Birthday);
                                            Task task = new Task(Tasks[i].Name);
                                            Employees[e].AssignedTasks.Add(task);
                                            Tasks[i].WorkersAssigned.Add(emp);
                                            SaveEmployee(Employees);
                                            SaveTasks(Tasks);
                                            check = false;
                                        }
                                    }
                                    if (check) { Console.WriteLine("That name does not exist"); }
                                }
                            }
                        }
                    }
                    else Console.WriteLine("There are no active tasks");

                    
                    Console.WriteLine("========================================\nTo continue press enter...");
                    Console.ReadLine();
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("2. Show Completed tasks");
                    Console.WriteLine("========================================");
                    LoadTasks();

                    if (Tasks != null)
                    {
                        foreach (Task t in Tasks)
                        {
                            if (t.Complete) { Console.WriteLine(t.ToString()); }
                            
                        }
                    }
                    else Console.WriteLine("There are no Completed Tasks");


                    Console.WriteLine("========================================\nTo continue press enter...");
                    Console.ReadLine();
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("3. Create Task");
                    Console.WriteLine("========================================");
                    Console.Write("Please enter the name of the task you would like to create: ");
                    string name = Console.ReadLine();
                    Task newTask = new Task(name);
                    Tasks.Add(newTask);
                    SaveTasks(Tasks);
                    Console.WriteLine("Task added successfully");
                    Console.WriteLine("========================================\nTo continue press enter...");
                    Console.ReadLine();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("4. Return to Main Menu");
                    Console.WriteLine("========================================");
                    break;
                default:
                    Console.WriteLine("Invalid user input, please try again");
                    break;



            }
        }

        public static void ShowEmployeeMenu()
        {
            Console.WriteLine("=============== Employee Menu ==============");
            Console.WriteLine("1. Show All Employees");
            Console.WriteLine("2. Create Employee");
            Console.WriteLine("3. Delete Employee");
            Console.WriteLine("4. Return to Main Menu");
            Console.WriteLine("============================================");

            int userInput = Check(Console.ReadLine()); //tryparse didnt work? use the out reference?

            switch (userInput)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("1. Show All Employees: ");
                    Console.WriteLine("========================================");
                    LoadEmployee();
                    if (Employees.Count != 0)
                    {
                        foreach(Employee e in Employees)
                        {
                            Console.WriteLine(e);
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no employees");
                    }
                    Console.WriteLine("========================================\nTo continue press enter...");
                    Console.ReadLine();
                    break;

                //for each employee in employee list have that employee object print its staus
                case 2:
                    Console.Clear();
                    Console.WriteLine("2. Create an Employee: ");
                    Console.WriteLine("========================================");

                    Console.Write("Please enter name: ");
                    string name = Console.ReadLine().ToLower();
                    Console.Write("Please enter employee birthday in MM\\DD\\YYYY format:\nMonth: ");
                    int month = Check(Console.ReadLine());
                    Console.Write("Day: ");
                    int day = Check(Console.ReadLine());
                    Console.Write("Year: ");
                    int year = Check(Console.ReadLine());

                    DateTime birthday = new DateTime(year, month, day);
                    Employee newEmployee = new Employee(name, birthday);
                    Employees.Add(newEmployee);
                    SaveEmployee(Employees);
                    Console.WriteLine("========================================\nTo continue press enter...");
                    Console.ReadLine();
                    break;
                //call a method called createEmployee() from the employee class and add it to a list
                case 3:
                    Console.WriteLine("3. Delete Employee");
                    Console.WriteLine("========================================");

                    Console.Write("Please enter employee name: ");
                    string delete = Console.ReadLine().ToLower();

                    bool t = true;
                    for (int i = 0; i < Employees.Count; i++)
                    {
                        if (Employees[i].Name == delete)
                        {
                            Employees.Remove(Employees[i]);
                            Console.WriteLine($"{delete} has been removed");
                            t = false;
                            SaveEmployee(Employees);
                        }
                    }
                    if (t)
                    {
                        Console.WriteLine("That name does not exist");
                    }

                    Console.WriteLine("========================================\nTo continue press enter...");
                    Console.ReadLine();
                    break;
                //search the employee list and find if the named employee exists, then remove it
                case 4:
                    Console.WriteLine("4. Return to Main Menu");
                    Console.WriteLine("========================================");
                    break;
                default:
                    Console.WriteLine("Invalid user input, please try again");
                    break;
            }

        }

    }
}