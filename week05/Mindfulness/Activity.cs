using System;
using System.Collections.Generic;

abstract class Activity
{
    protected string _name;
    protected string _description;
    protected int _duration;
    private PromptSelector _promptSelector = new PromptSelector();

    public virtual void Start()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name} Activity!");
        Console.WriteLine($"\n{_description}");
        Console.Write("\nHow long (in seconds) would you like to do this activity? ");

        while (!int.TryParse(Console.ReadLine(), out _duration) || _duration <= 0)
        {
            Console.Write("Please enter a valid number of seconds: ");
        }

        Console.WriteLine("\nPrepare yourself...");
        ShowAnimation(3);
    }

    public virtual void End()
    {
        Console.WriteLine($"\nWell done!");
        Console.WriteLine($"You completed the {_name} Activity for {_duration} seconds.");
        ShowAnimation(3);
    }

    public abstract void RunActivity();

    public string GetName() => _name;

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
        return _promptSelector.GetNextPrompt(prompts);
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