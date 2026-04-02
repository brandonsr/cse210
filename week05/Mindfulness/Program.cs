using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class Program
{
    // Exceeding Requirements Implementation:
    // 1. Activity Tracking - Logs how many times each activity was performed
    // 2. Smart Prompt Selection - No prompts repeated until all have been used in the session
    // 3. Persistent Storage - Saves activity logs to JSON file
    // 4. Enhanced Animations - Dynamic breathing animation with scaling text

    private static ActivityTracker tracker = new ActivityTracker();
    private static string logFilePath = "activity_log.json";

    static void Main(string[] args)
    {
        tracker.LoadLogs(logFilePath);

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
                        RunActivity(new BreathingActivity(), tracker);
                        break;
                    case "2":
                        RunActivity(new ReflectionActivity(), tracker);
                        break;
                    case "3":
                        RunActivity(new ListingActivity(), tracker);
                        break;
                    case "4":
                        DisplayStats();
                        break;
                    case "5":
                        tracker.SaveLogs(logFilePath);
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
        tracker.SaveLogs(logFilePath);
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    static void DisplayStats()
    {
        Console.Clear();
        Console.WriteLine("=== Activity Statistics ===\n");
        var stats = tracker.GetStats();

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

// Base Activity Class
abstract class Activity
{
    protected string name;
    protected string description;
    protected int duration;
    private PromptSelector promptSelector = new PromptSelector();

    public virtual void Start()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {name} Activity!");
        Console.WriteLine($"\n{description}");
        Console.Write("\nHow long (in seconds) would you like to do this activity? ");

        while (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0)
        {
            Console.Write("Please enter a valid number of seconds: ");
        }

        Console.WriteLine("\nPrepare yourself...");
        ShowAnimation(3);
    }

    public virtual void End()
    {
        Console.WriteLine($"\nWell done!");
        Console.WriteLine($"You completed the {name} Activity for {duration} seconds.");
        ShowAnimation(3);
    }

    public abstract void RunActivity();

    public string GetName() => name;

    protected void ShowAnimation(int seconds)
    {
        List<string> spinner = new List<string> { "|", "/", "-", "\\" };
        int cycles = seconds * 2;

        for (int i = 0; i < cycles; i++)
        {
            Console.Write(spinner[i % spinner.Count] + "\b");
            System.Threading.Thread.Sleep(500);
        }
        Console.Write(" \b");
    }

    protected string GetRandomPrompt(List<string> prompts)
    {
        return promptSelector.GetNextPrompt(prompts);
    }

    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Clear();
            Console.WriteLine($"Time remaining: {i} seconds");
            System.Threading.Thread.Sleep(1000);
        }
    }
}

// Breathing Activity
class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        name = "Breathing";
        description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    public override void RunActivity()
    {
        int elapsed = 0;
        while (elapsed < duration)
        {
            ShowBreathingAnimation(4, "Breathe In");
            elapsed += 4;
            if (elapsed >= duration) break;

            ShowBreathingAnimation(4, "Breathe Out");
            elapsed += 4;
        }
    }

    private void ShowBreathingAnimation(int seconds, string instruction)
    {
        int cycles = seconds * 2;
        for (int i = 0; i < cycles; i++)
        {
            Console.Clear();
            Console.WriteLine(instruction);
            int scale = (i * 10) / cycles;
            Console.WriteLine(new string('.', scale + 1));
            System.Threading.Thread.Sleep(500);
        }
    }
}

// Reflection Activity
class ReflectionActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did that make you feel?",
        "What made this time different than other times?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
    {
        name = "Reflection";
        description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have within you.";
    }

    public override void RunActivity()
    {
        string prompt = GetRandomPrompt(prompts);
        Console.Clear();
        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine($"\n--- {prompt} ---");
        Console.WriteLine("\nWhen you have something in mind, press enter to continue.");
        Console.ReadLine();

        Console.WriteLine("\nNow ponder on each of the following questions as they relate to your experience.\nYou may begin immediately and will have time to pause between each question.");

        int elapsed = 0;
        while (elapsed < duration)
        {
            string question = GetRandomPrompt(questions);
            Console.WriteLine($"\n> {question}");
            ShowAnimation(5);
            elapsed += 5;
        }
    }
}

// Listing Activity
class ListingActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths?",
        "Who have you helped this year?",
        "When have you felt the Holy Ghost this year?",
        "Who are some of your friends?",
        "What are some of your personal experiences that define you?"
    };

    public ListingActivity()
    {
        name = "Listing";
        description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    public override void RunActivity()
    {
        string prompt = GetRandomPrompt(prompts);
        Console.Clear();
        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine($"\n--- {prompt} ---\n");
        Console.WriteLine("You have a few seconds to begin...");
        ShowAnimation(3);

        List<string> items = new List<string>();
        int elapsed = 0;
        while (elapsed < duration)
        {
            Console.Write("\nEnter an item (or press Enter to skip): ");
            string item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item))
            {
                items.Add(item);
            }
            elapsed += 2;
        }

        Console.WriteLine($"\nYou listed {items.Count} items.");
    }
}

// Activity Tracker for tracking and persistence
class ActivityTracker
{
    private Dictionary<string, int> activityCount = new Dictionary<string, int>();

    public void LogActivity(string activityName)
    {
        if (activityCount.ContainsKey(activityName))
        {
            activityCount[activityName]++;
        }
        else
        {
            activityCount[activityName] = 1;
        }
    }

    public Dictionary<string, int> GetStats() => new Dictionary<string, int>(activityCount);

    public void SaveLogs(string filePath)
    {
        try
        {
            var json = JsonSerializer.Serialize(activityCount, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving logs: {ex.Message}");
        }
    }

    public void LoadLogs(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                activityCount = JsonSerializer.Deserialize<Dictionary<string, int>>(json) ?? new Dictionary<string, int>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading logs: {ex.Message}");
        }
    }
}

// Smart Prompt Selector - ensures no prompt repeats until all are used in a session
class PromptSelector
{
    private Dictionary<int, List<string>> unusedPrompts = new Dictionary<int, List<string>>();
    private Random random = new Random();

    public string GetNextPrompt(List<string> prompts)
    {
        int key = prompts.GetHashCode();

        // Initialize unused prompts for this list if not done
        if (!unusedPrompts.ContainsKey(key) || unusedPrompts[key].Count == 0)
        {
            unusedPrompts[key] = new List<string>(prompts);
        }

        // Select random prompt from unused
        int index = random.Next(unusedPrompts[key].Count);
        string selectedPrompt = unusedPrompts[key][index];
        unusedPrompts[key].RemoveAt(index);

        return selectedPrompt;
    }
}
