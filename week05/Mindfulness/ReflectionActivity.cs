using System;
using System.Collections.Generic;

class ReflectionActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new List<string>
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
        _name = "Reflection";
        _description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have within you.";
    }

    public override void RunActivity()
    {
        string prompt = GetRandomPrompt(_prompts);
        Console.Clear();
        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine($"\n--- {prompt} ---");
        Console.WriteLine("\nWhen you have something in mind, press enter to continue.");
        Console.ReadLine();

        Console.WriteLine("\nNow ponder on each of the following questions as they relate to your experience.\nYou may begin immediately and will have time to pause between each question.");

        int elapsed = 0;
        while (elapsed < _duration)
        {
            string question = GetRandomPrompt(_questions);
            Console.WriteLine($"\n> {question}");
            ShowAnimation(5);
            elapsed += 5;
        }
    }
}