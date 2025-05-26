// This is a beginner-friendly version of the Student Score App
// It is written in a clear and simple style with lots of comments to aid understanding

using System;
using System.Collections.Generic;

namespace StudentScoreApp
{
    // This class defines the structure of a Student
    class Student
    {
        public string Name = ""; // Student's name
        public string ID = "";   // Unique student ID

        // Dictionary to hold subject names and their respective scores
        public Dictionary<string, int> Scores = new Dictionary<string, int>();

        public int Total = 0; // Total score across subjects
        public double Average = 0.0; // Average score
        public string Grade = ""; // Grade based on average
    }

    class Program
    {
        // List to store all student records
        static List<Student> students = new List<Student>();

        // Predefined list of subjects
        static List<string> subjects = new List<string> { "Math", "English", "Science" };

        static void Main(string[] args)
        {
            // Boolean to control menu loop
            bool running = true;

            // Keep running until the user decides to exit
            while (running)
            {
                Console.WriteLine("\nSTUDENT SCORE APPLICATION");
                Console.WriteLine("1. Add Student and Scores");
                Console.WriteLine("2. View All Student Results");
                Console.WriteLine("3. Search by ID or Name");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option (1-4): ");

                string choice = Console.ReadLine() ?? ""; // Get user input for menu choice

                if (choice == "1")
                {
                    AddStudent(); // Add a new student
                }
                else if (choice == "2")
                {
                    ViewAllResults(); // Show all student results
                }
                else if (choice == "3")
                {
                    SearchStudent(); // Find a student by ID or Name
                }
                else if (choice == "4")
                {
                    running = false; // Exit the application
                }
                else
                {
                    Console.WriteLine("Invalid option. Please choose from 1 to 4.");
                }
            }
        }

        // Function to add a new student
        static void AddStudent()
        {
            Student student = new Student();

            // Ask for the student's name and validate it
            while (true)
            {
                Console.Write("Enter Student Name (letters only): ");
                string name = Console.ReadLine() ?? ""; // Read input and ensure it's not null

                if (!string.IsNullOrWhiteSpace(name) && IsAllLetters(name))
                {
                    student.Name = name;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid name. Only letters are allowed.");
                }
            }

            // Ask for a unique student ID
            while (true)
            {
                Console.Write("Enter Unique Student ID: ");
                string id = Console.ReadLine() ?? ""; // Read input and ensure it's not null

                if (!string.IsNullOrWhiteSpace(id) && IsUniqueID(id))
                {
                    student.ID = id;
                    break;
                }
                else
                {
                    Console.WriteLine("ID is either empty or already taken. Try another.");
                }
            }

            // Get scores for each subject
            foreach (string subject in subjects)
            {
                while (true)
                {
                    Console.Write($"Enter score for {subject} (0 - 100): ");
                    string input = Console.ReadLine() ?? ""; // Read input and ensure it's not null

                    if (int.TryParse(input, out int score) && score >= 0 && score <= 100)
                    {
                        student.Scores[subject] = score;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid score. Please enter a number from 0 to 100.");
                    }
                }
            }

            // Calculate the total score
            int total = 0;
            foreach (var score in student.Scores.Values)
            {
                total += score;
            }
            student.Total = total;

            // Calculate average score
            student.Average = (double)total / subjects.Count;

            // Assign grade based on average
            student.Grade = GetGrade(student.Average);

            // Add the student to the list
            students.Add(student);

            Console.WriteLine("Student successfully added!\n");
        }

        // Function to view all student results in table format
        static void ViewAllResults()
        {
            Console.WriteLine("\nName      ID       Math   English  Science  Total  Average  Grade");
            Console.WriteLine("---------------------------------------------------------------");

            foreach (var s in students)
            {
                Console.Write(s.Name.PadRight(10));
                Console.Write(s.ID.PadRight(9));

                foreach (var subject in subjects)
                {
                    Console.Write(s.Scores[subject].ToString().PadRight(8));
                }

                Console.Write(s.Total.ToString().PadRight(7));
                Console.Write(s.Average.ToString("F2").PadRight(9));
                Console.WriteLine(s.Grade);
            }
        }

        // Function to search for a student by ID or name
        static void SearchStudent()
        {
            Console.Write("Enter Student Name or ID to search: ");
            string input = Console.ReadLine() ?? ""; // Read input and ensure it's not null

            bool found = false;

            foreach (var s in students)
            {
                if (s.Name.Equals(input, StringComparison.OrdinalIgnoreCase) || s.ID == input)
                {
                    Console.WriteLine("\nName      ID       Math   English  Science  Total  Average  Grade");
                    Console.WriteLine("---------------------------------------------------------------");

                    Console.Write(s.Name.PadRight(10));
                    Console.Write(s.ID.PadRight(9));

                    foreach (var subject in subjects)
                    {
                        Console.Write(s.Scores[subject].ToString().PadRight(8));
                    }

                    Console.Write(s.Total.ToString().PadRight(7));
                    Console.Write(s.Average.ToString("F2").PadRight(9));
                    Console.WriteLine(s.Grade);

                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Student not found.");
            }
        }

        // Function to determine grade from average score
        static string GetGrade(double average)
        {
            if (average >= 70) return "A";
            else if (average >= 60) return "B";
            else if (average >= 50) return "C";
            else if (average >= 45) return "D";
            else if (average >= 40) return "E";
            else return "F";
        }

        // Function to check if a string contains only letters
        static bool IsAllLetters(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c)) return false;
            }
            return true;
        }

        // Function to ensure the entered ID is unique
        static bool IsUniqueID(string id)
        {
            foreach (var s in students)
            {
                if (s.ID == id)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
