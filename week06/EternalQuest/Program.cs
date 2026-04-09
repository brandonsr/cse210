using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        GoalTracker tracker = new GoalTracker();
        const string filename = "goals.txt";

        // Load existing goals
        tracker.LoadFromFile(filename);

        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine($"Total Score: {tracker.GetTotalScore()} points");
            Console.WriteLine("\nGoals:");
            DisplayGoals(tracker.GetGoals());
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. Record Goal Event");
            Console.WriteLine("3. Save and Quit");
            Console.Write("\nChoose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateNewGoal(tracker);
                    break;
                case "2":
                    RecordGoalEvent(tracker);
                    break;
                case "3":
                    tracker.SaveToFile(filename);
                    running = false;
                    Console.WriteLine("Goals saved. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    PauseForUser();
                    break;
            }
        }
    }

    static void DisplayGoals(List<Goal> goals)
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals yet. Create one to get started!");
            return;
        }

        for (int i = 0; i < goals.Count; i++)
        {
            Goal goal = goals[i];
            Console.WriteLine($"{i + 1}. {goal.GetDetails()} {goal.GetName()} - {goal.GetDescription()}");
        }
    }

    static void CreateNewGoal(GoalTracker tracker)
    {
        Console.Clear();
        Console.WriteLine("Choose goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter choice: ");

        string choice = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();

        Console.Write("Enter points for this goal: ");
        int points = GetValidInt();

        Goal goal = null;

        switch (choice)
        {
            case "1":
                goal = new SimpleGoal(name, description, points);
                break;
            case "2":
                goal = new EternalGoal(name, description, points);
                break;
            case "3":
                Console.Write("Enter required count to complete: ");
                int requiredCount = GetValidInt();
                Console.Write("Enter bonus points for completion: ");
                int bonusPoints = GetValidInt();
                goal = new ChecklistGoal(name, description, points, requiredCount, bonusPoints);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                PauseForUser();
                return;
        }

        if (goal != null)
        {
            tracker.AddGoal(goal);
            Console.WriteLine("Goal created successfully!");
            PauseForUser();
        }
    }

    static void RecordGoalEvent(GoalTracker tracker)
    {
        Console.Clear();
        List<Goal> goals = tracker.GetGoals();

        if (goals.Count == 0)
        {
            Console.WriteLine("No goals to record events for.");
            PauseForUser();
            return;
        }

        DisplayGoals(goals);
        Console.Write("\nSelect goal number to record event: ");

        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= goals.Count)
        {
            Goal selectedGoal = goals[choice - 1];
            int scoreBefore = tracker.GetTotalScore();

            tracker.RecordGoalEvent(choice - 1);

            int scoreAfter = tracker.GetTotalScore();
            int pointsEarned = scoreAfter - scoreBefore;

            if (pointsEarned > 0)
            {
                Console.WriteLine($"Event recorded! You earned {pointsEarned} points!");
            }
            else
            {
                Console.WriteLine("This goal is already complete. No more points can be earned.");
            }
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }

        PauseForUser();
    }

    static void PauseForUser()
    {
        if (Console.IsInputRedirected)
        {
            return;
        }
        Console.WriteLine("Press any key to continue...");
        try
        {
            Console.ReadKey(true);
        }
        catch
        {
            // Silently handle if ReadKey fails
        }
    }

    static int GetValidInt()
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                return result;
            }
            Console.Write("Invalid input. Please enter a valid number: ");
        }
    }
}