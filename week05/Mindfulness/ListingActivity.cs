using System;
using System.Collections.Generic;

class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
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
        _name = "Listing";
        _description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    public override void RunActivity()
    {
        string prompt = GetRandomPrompt(_prompts);
        Console.Clear();
        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine($"\n--- {prompt} ---\n");
        Console.WriteLine("You have a few seconds to begin...");
        ShowAnimation(3);

        List<string> items = new List<string>();
        int elapsed = 0;
        while (elapsed < _duration)
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