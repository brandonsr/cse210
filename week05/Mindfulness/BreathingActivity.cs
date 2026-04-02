using System;

class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        _name = "Breathing";
        _description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    public override void RunActivity()
    {
        int elapsed = 0;
        while (elapsed < _duration)
        {
            ShowBreathingAnimation(4, "Breathe In");
            elapsed += 4;
            if (elapsed >= _duration) break;

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