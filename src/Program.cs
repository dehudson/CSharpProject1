using System;
using System.Collections.Generic;
using src;
using System.Text.Json;
using System.IO;

namespace Destin.CodeLou.ExerciseProjec
{
    class Program
    {
        static string _studentRepositoryPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\sutdent.json";
        static List<Student> studentList = File.Exists(_studentRepositoryPath) ? Read() : new List<Student>();

        static async void Save()
        {
            using (var file = File.CreateText(_studentRepositoryPath))
            {
                await file.WriteAsync(JsonSerializer.Serialize(studentList));
            }
        }

        static List<Student> Read()
        {
            return JsonSerializer.Deserialize<List<Student>>(File.ReadAllText(_studentRepositoryPath));
        }

        static bool stillRunning = true;
        
        static void Main(string[] args)
        {
            while (stillRunning)
            {
                MainMenu();
            }
        }

        static void MainMenu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1. New Student");
            Console.WriteLine("2. List Students");
            Console.WriteLine("3. Find Student By Name");
            Console.WriteLine("4. Search Students By Class");
            Console.WriteLine("5. Exit");

            int selection;
            Int32.TryParse(Console.ReadLine(), out selection);

            switch (selection)
            {
                case 1:
                    InputStudent();
                    break;
                case 2:
                    ListStudents();
                    break;
                case 3:
                    SearchByName();
                    break;
                case 4:
                    SearchByClass();
                    break;
                case 5:
                    Console.WriteLine("Goodbye!");
                    stillRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    Console.ReadKey();
                    break;
            }
        }

        static void InputStudent()
        {
            bool stillAdding = true;

            while (stillAdding)
            {
                var studentRecord = new Student();
                Console.WriteLine("Enter Student Id");
                while (true)
                {
                    if (Int32.TryParse(Console.ReadLine(), out var studentId))
                    {
                        studentRecord.StudentId = studentId;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid Student ID");
                    }
                }
                Console.WriteLine("Enter First Name");
                var studentFirstName = Console.ReadLine();
                Console.WriteLine("Enter Last Name");
                var studentLastName = Console.ReadLine();
                Console.WriteLine("Enter Class Name");
                var className = Console.ReadLine();
                Console.WriteLine("Enter Last Class Completed");
                var lastClass = Console.ReadLine();
                Console.WriteLine("Enter Last Class Completed Date in format MM/dd/YYYY");
                while (true)
                {
                    if (DateTimeOffset.TryParse(Console.ReadLine(), out var lastCompletedOn))
                    {
                        studentRecord.LastClassCompletedOn = lastCompletedOn;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid date");
                    }
                }
                Console.WriteLine("Enter Start Date in format MM/dd/YYYY");
                while (true)
                {
                    if (DateTimeOffset.TryParse(Console.ReadLine(), out var startDate))
                    {
                        studentRecord.StartDate = startDate;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid date");
                    }
                }
                studentRecord.FirstName = studentFirstName;
                studentRecord.LastName = studentLastName;
                studentRecord.ClassName = className;
                studentRecord.LastClassCompleted = lastClass;
                Console.WriteLine($"Student Id | Name |  Class "); ;
                Console.WriteLine($"{studentRecord.StudentId} | {studentRecord.FirstName} {studentRecord.LastName} | {studentRecord.ClassName} "); ;
                Console.ReadKey();

                studentList.Add(studentRecord);
                Save();

                Console.WriteLine("Would you like to add another student? \"Yes\" or \"No\"");
                var answer = Console.ReadLine();
                if (answer.ToLower() == "yes")
                {
                    stillAdding = true;
                }
                else if (answer.ToLower() == "no")
                {
                    stillAdding = false;
                }
                else
                {
                    Console.WriteLine("Invalid response");
                    stillAdding = false;
                }
            }   
        }

        static void ListStudents()
        {
            Console.WriteLine($"Student Id | Name |  Class ");
            foreach(Student i in studentList)
            {
                Console.WriteLine($"{i.StudentId} | {i.FirstName} {i.LastName} | {i.ClassName} ");
            }
            Console.ReadKey();
        }

        static void SearchByName()
        {
            Console.WriteLine("Enter full name to search for");
            string nameToSearch = Console.ReadLine();
            bool studentFound = true;
            foreach(Student i in studentList)
            {
                string nameToCheck = $"{i.FirstName} {i.LastName}";
                if (nameToSearch == nameToCheck)
                {
                    if (studentFound == !true)
                    {
                        studentFound = true;
                    }
                    Console.WriteLine($"{i.StudentId} | {i.FirstName} {i.LastName} | {i.ClassName} ");
                    break;
                }
                else
                {
                    if (studentFound == !false)
                    {
                        studentFound = false;
                    }

                }
            }
            if (studentFound == false)
            {
                Console.WriteLine("Could not find user with that name");
            }
            Console.ReadKey();
        }

        static void SearchByClass()
        {
            Console.WriteLine("Enter class name to search for");
            string classToSearch = Console.ReadLine();
            bool studentFound = false;
            foreach(Student i in studentList)
            {
                string classToCheck = $"{i.ClassName}";
                if (classToSearch == classToCheck)
                {
                    if (studentFound == !true)
                    {
                        studentFound = true;
                    }
                    Console.WriteLine($"{i.StudentId} | {i.FirstName} {i.LastName} | {i.ClassName} ");
                    break;
                }
            }
            if (studentFound == false)
            {
                Console.WriteLine("Could not find any students for that class.");
            }
            Console.ReadKey();
        }
    }
}
