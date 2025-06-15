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
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nSTUDENT SCORE APPLICATION");
                Console.WriteLine("1. Add Student and Scores");
                Console.WriteLine("2. View All Student Results");
                Console.WriteLine("3. Search by ID or Name");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option (1-4): ");

                string choice = Console.ReadLine() ?? "";

                if (choice == "1")
                {
                    AddStudent();
                }
                else if (choice == "2")
                {
                    ViewAllResults();
                }
                else if (choice == "3")
                {
                    SearchStudent();
                }
                else if (choice == "4")
                {
                    running = false;
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
                Console.Write("Enter Student Name (letters and spaces only): ");
                string name = Console.ReadLine() ?? "";

                if (!string.IsNullOrWhiteSpace(name) && IsValidName(name))
                {
                    student.Name = name;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid name. Use letters and spaces only. Name cannot be empty or just spaces.");
                }
            }

            // Ask for a unique student ID
            while (true)
            {
                Console.Write("Enter Unique Student ID: ");
                string id = Console.ReadLine() ?? "";

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
                    string input = Console.ReadLine() ?? "";

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

            // Calculate total and average
            int total = 0;
            foreach (var score in student.Scores.Values)
            {
                total += score;
            }
            student.Total = total;
            student.Average = (double)total / subjects.Count;
            student.Grade = GetGrade(student.Average);

            students.Add(student);
            Console.WriteLine("Student successfully added!\n");
        }

        // Function to view all student results
        static void ViewAllResults()
        {
            Console.WriteLine("\nName           ID         Math     English  Science  Total  Average  Grade");
            Console.WriteLine("--------------------------------------------------------------------------");

            foreach (var s in students)
            {
                Console.Write(s.Name.PadRight(15));
                Console.Write(s.ID.PadRight(10));

                foreach (var subject in subjects)
                {
                    Console.Write(s.Scores[subject].ToString().PadRight(9));
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
            string input = Console.ReadLine() ?? "";

            bool found = false;

            foreach (var s in students)
            {
                if (s.Name.Equals(input, StringComparison.OrdinalIgnoreCase) || s.ID == input)
                {
                    Console.WriteLine("\nName           ID         Math     English  Science  Total  Average  Grade");
                    Console.WriteLine("--------------------------------------------------------------------------");

                    Console.Write(s.Name.PadRight(15));
                    Console.Write(s.ID.PadRight(10));

                    foreach (var subject in subjects)
                    {
                        Console.Write(s.Scores[subject].ToString().PadRight(9));
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

        // Function to check if a string contains only letters and spaces, and not just spaces
        static bool IsValidName(string input)
        {
            foreach (char c in input)
            {
                if (!(char.IsLetter(c) || c == ' '))
                    return false;
            }
            return input.Trim().Length > 0;
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
