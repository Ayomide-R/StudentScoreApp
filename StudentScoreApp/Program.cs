// A simple console app to manage student scores (written like a beginner)
using System;
using System.Collections.Generic;

namespace StudentScoreApp
{
    // Class to hold student information
    class Student
    {
        public string Name { get; set; } = string.Empty; // Student's name
        public string ID { get; set; } = string.Empty;   // Student's ID
        public Dictionary<string, int> Scores { get; set; } = new(); // Subjects and scores
        public int Total { get; set; } // Total of all scores
        public double Average { get; set; } // Average of scores
        public string Grade { get; set; } = string.Empty; // Grade based on average
    }

    class Program
    {
        // A list to store all student records
        static List<Student> students = new List<Student>();

        // Subjects for which scores will be entered
        static List<string> subjects = new List<string> { "Math", "English", "Science" };

        static void Main(string[] args)
        {
            bool running = true; // To keep the app running until user chooses to exit

            // Keep showing menu until user exits
            while (running)
            {
                Console.WriteLine("\n=== STUDENT SCORE SYSTEM ===");
                Console.WriteLine("1. Add Student and Scores");
                Console.WriteLine("2. View All Student Results");
                Console.WriteLine("3. Search Student by ID or Name");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine() ?? ""; // Get user choice, avoid null
                Console.WriteLine(); // Empty line for better spacing

                // Handle user choice
                switch (input)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        DisplayAllStudents();
                        break;
                    case "3":
                        SearchStudent();
                        break;
                    case "4":
                        running = false; // Stop the loop
                        Console.WriteLine("Exiting application...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        // Method to add a new student and their scores
        static void AddStudent()
        {
            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine() ?? ""; // Get student name safely

            Console.Write("Enter Student ID: ");
            string id = Console.ReadLine() ?? ""; // Get student ID safely

            Dictionary<string, int> scores = new Dictionary<string, int>(); // To hold scores for subjects

            // Loop through each subject and get score
            foreach (var subject in subjects)
            {
                int score;
                while (true) // Loop until valid input
                {
                    Console.Write($"Enter score for {subject}: ");
                    string scoreInput = Console.ReadLine() ?? ""; // Get score input

                    // Try to convert score to int
                    if (int.TryParse(scoreInput, out score) && score >= 0 && score <= 100)
                    {
                        scores[subject] = score; // Add score to dictionary
                        break; // Break loop if valid
                    }
                    else
                    {
                        Console.WriteLine("Invalid score. Enter a number between 0 and 100.");
                    }
                }
            }

            // Create new student object
            Student student = new Student();
            student.Name = name;
            student.ID = id;
            student.Scores = scores;
            student.Total = CalculateTotal(scores); // Get total
            student.Average = CalculateAverage(scores); // Get average
            student.Grade = AssignGrade(student.Average); // Get grade

            students.Add(student); // Add student to list

            Console.WriteLine("Student added successfully.");
        }

        // Method to calculate total score
        static int CalculateTotal(Dictionary<string, int> scores)
        {
            int total = 0; // Start from 0
            foreach (var score in scores.Values)
            {
                total += score; // Add each subject score
            }
            return total; // Return total
        }

        // Method to calculate average
        static double CalculateAverage(Dictionary<string, int> scores)
        {
            if (scores.Count == 0)
                return 0; // Avoid division by 0

            return (double)CalculateTotal(scores) / scores.Count; // Calculate average
        }

        // Method to assign grade based on average
        static string AssignGrade(double average)
        {
            if (average >= 70) return "A";
            else if (average >= 60) return "B";
            else if (average >= 50) return "C";
            else if (average >= 45) return "D";
            else if (average >= 40) return "E";
            else return "F";
        }

        // Method to display all students
        static void DisplayAllStudents()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No student data available.");
                return; // Exit method if no data
            }

            // Print table header
            Console.WriteLine("Name\tID\t\tMath\tEnglish\tScience\tTotal\tAverage\tGrade");

            // Loop through each student
            foreach (var student in students)
            {
                Console.Write($"{student.Name}\t{student.ID}\t");

                // Print subject scores
                foreach (var subject in subjects)
                {
                    Console.Write($"{student.Scores.GetValueOrDefault(subject, 0)}\t");
                }

                // Print total, average and grade
                Console.WriteLine($"{student.Total}\t{student.Average:F2}\t{student.Grade}");
            }
        }

        // Method to search student by name or ID
        static void SearchStudent()
        {
            Console.Write("Enter Student ID or Name to search: ");
            string keyword = Console.ReadLine() ?? ""; // Get search keyword

            // Try to find student that matches name or ID
            var found = students.Find(s =>
                s.ID.Equals(keyword, StringComparison.OrdinalIgnoreCase) ||
                s.Name.Equals(keyword, StringComparison.OrdinalIgnoreCase));

            // If found show details
            if (found != null)
            {
                Console.WriteLine("\nStudent Found:");
                Console.WriteLine($"Name: {found.Name}");
                Console.WriteLine($"ID: {found.ID}");

                // Print scores
                foreach (var subject in subjects)
                {
                    Console.WriteLine($"{subject}: {found.Scores.GetValueOrDefault(subject, 0)}");
                }

                // Print totals
                Console.WriteLine($"Total: {found.Total}");
                Console.WriteLine($"Average: {found.Average:F2}");
                Console.WriteLine($"Grade: {found.Grade}");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }
    }
}
