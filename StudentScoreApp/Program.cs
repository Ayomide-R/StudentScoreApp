using System; // For basic console input/output
using System.Collections.Generic; // For List and Dictionary
using System.Linq; // For using LINQ methods like Sum and Where

// Define a Student class to hold student data
class Student
{
    // Public properties of the student
    public string Name { get; set; }         // Student's full name
    public string ID { get; set; }           // Unique student ID
    public Dictionary<string, int> Scores { get; set; } // Subject scores
    public int Total { get; set; }           // Total score
    public double Average { get; set; }      // Average score
    public string Grade { get; set; }        // Grade based on average

    // Constructor to initialize a student with name, ID, and scores
    public Student(string name, string id, Dictionary<string, int> scores)
    {
        Name = name;
        ID = id;
        Scores = scores;

        CalculateTotalAndAverage(); // Automatically calculate total and average
        AssignGrade();              // Automatically assign grade
    }

    // Method to calculate total and average
    private void CalculateTotalAndAverage()
    {
        Total = Scores.Values.Sum();                 // Add all subject scores
        Average = (double)Total / Scores.Count;      // Divide total by number of subjects
    }

    // Method to assign a grade based on the average
    private void AssignGrade()
    {
        if (Average >= 70)
            Grade = "A";
        else if (Average >= 60)
            Grade = "B";
        else if (Average >= 50)
            Grade = "C";
        else if (Average >= 45)
            Grade = "D";
        else if (Average >= 40)
            Grade = "E";
        else
            Grade = "F";
    }
}

// Main class for the application
class Program
{
    // List to store all students in memory
    static List<Student> students = new List<Student>();

    // Subjects to be used for each student
    static string[] subjects = { "Math", "English", "Science" };

    // Entry point of the program
    static void Main(string[] args)
    {
        while (true) // Loop until the user chooses to exit
        {
            Console.Clear(); // Clear the console screen
            Console.WriteLine("=== Student Score Management System ===");
            Console.WriteLine("1. Add Student and Scores");
            Console.WriteLine("2. View All Student Results");
            Console.WriteLine("3. Search by ID or Name");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option (1-4): ");
            string option = Console.ReadLine(); // Read user's choice

            // Use switch to handle different menu options
            switch (option)
            {
                case "1":
                    AddStudent(); // Call method to add a student
                    break;
                case "2":
                    DisplayAllStudents(); // Call method to show all results
                    break;
                case "3":
                    SearchStudent(); // Call method to search for a student
                    break;
                case "4":
                    Console.WriteLine("Exiting application...");
                    return; // Exit the program
                default:
                    Console.WriteLine("Invalid option. Press any key to try again.");
                    Console.ReadKey(); // Wait for user to press a key
                    break;
            }
        }
    }

    // Method to add a new student
    static void AddStudent()
    {
        Console.Clear(); // Clear screen
        Console.WriteLine("Enter Student Details:");
        Console.Write("Name: ");
        string name = Console.ReadLine(); // Get student's name
        Console.Write("ID: ");
        string id = Console.ReadLine(); // Get student ID

        // Create a dictionary to hold subject scores
        var scores = new Dictionary<string, int>();

        // Loop through each subject to get scores
        foreach (var subject in subjects)
        {
            int score;
            while (true)
            {
                Console.Write($"{subject} Score: ");
                // Try to parse input and ensure it's between 0 and 100
                if (int.TryParse(Console.ReadLine(), out score) && score >= 0 && score <= 100)
                    break;
                else
                    Console.WriteLine("Invalid input. Enter a score between 0 and 100.");
            }
            scores[subject] = score; // Add subject and score to dictionary
        }

        // Create a new student and add to the list
        students.Add(new Student(name, id, scores));

        Console.WriteLine("Student added successfully! Press any key to continue...");
        Console.ReadKey(); // Wait for user to continue
    }

    // Method to display all student results
    static void DisplayAllStudents()
    {
        Console.Clear(); // Clear screen

        // Check if there are any students
        if (students.Count == 0)
        {
            Console.WriteLine("No student data available.");
        }
        else
        {
            // Print table headers
            Console.WriteLine("Student Results:");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine("Name\tID\tMath\tEnglish\tScience\tTotal\tAverage\tGrade");
            Console.WriteLine("-------------------------------------------------------------------------------------");

            // Loop through each student and print their details
            foreach (var s in students)
            {
                Console.WriteLine($"{s.Name}\t{s.ID}\t{s.Scores["Math"]}\t{s.Scores["English"]}\t{s.Scores["Science"]}\t{s.Total}\t{s.Average:F2}\t{s.Grade}");
            }
        }

        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey(); // Wait for user
    }

    // Method to search for a student by ID or Name
    static void SearchStudent()
    {
        Console.Clear(); // Clear screen
        Console.Write("Enter Student ID or Name to search: ");
        string keyword = Console.ReadLine().ToLower(); // Read input and convert to lowercase

        // Use LINQ to find matching students
        var found = students.Where(s => s.ID.ToLower() == keyword || s.Name.ToLower().Contains(keyword)).ToList();

        // Check if any results found
        if (found.Count == 0)
        {
            Console.WriteLine("Student not found.");
        }
        else
        {
            // Display matched results
            Console.WriteLine("Search Results:");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine("Name\tID\tMath\tEnglish\tScience\tTotal\tAverage\tGrade");
            Console.WriteLine("-------------------------------------------------------------------------------------");

            foreach (var s in found)
            {
                Console.WriteLine($"{s.Name}\t{s.ID}\t{s.Scores["Math"]}\t{s.Scores["English"]}\t{s.Scores["Science"]}\t{s.Total}\t{s.Average:F2}\t{s.Grade}");
            }
        }

        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey(); // Wait for user
    }
}
