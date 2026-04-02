using System;
using System.Linq;

class Program
{
    // Exceeding Requirements Implementation:
    // 1. Activity Tracking - Logs how many times each activity was performed
    // 2. Smart Prompt Selection - No prompts repeated until all have been used in the session
    // 3. Persistent Storage - Saves activity logs to JSON file
    // 4. Enhanced Animations - Dynamic breathing animation with scaling text

    private static ActivityTracker _tracker = new ActivityTracker();
    private static string _logFilePath = "activity_log.json";

    static void Main(string[] args)
    {
        _tracker.LoadLogs(_logFilePath);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Mindfulness Activities ===");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. View Activity Stats");
            Console.WriteLine("5. Exit");
            Console.Write("\nSelect an activity (1-5): ");

            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        RunActivity(new BreathingActivity(), _tracker);
                        break;
                    case "2":
                        RunActivity(new ReflectionActivity(), _tracker);
                        break;
                    case "3":
                        RunActivity(new ListingActivity(), _tracker);
                        break;
                    case "4":
                        DisplayStats();
                        break;
                    case "5":
                        _tracker.SaveLogs(_logFilePath);
                        Console.WriteLine("\nActivity logs saved. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ReadKey();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }
    }

    static void RunActivity(Activity activity, ActivityTracker tracker)
    {
        activity.Start();
        activity.RunActivity();
        activity.End();
        tracker.LogActivity(activity.GetName());
        tracker.SaveLogs(_logFilePath);
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    static void DisplayStats()
    {
        Console.Clear();
        Console.WriteLine("=== Activity Statistics ===\n");
        var stats = _tracker.GetStats();

        if (stats.Count == 0)
        {
            Console.WriteLine("No activities logged yet.");
        }
        else
        {
            foreach (var stat in stats)
            {
                Console.WriteLine($"{stat.Key}: {stat.Value} time(s)");
            }
            Console.WriteLine($"\nTotal activities completed: {stats.Values.Sum()}");
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}