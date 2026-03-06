using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your grade percentage: ");
        double grade = double.Parse(Console.ReadLine());

        // Core Requirement 3: assign letter grade to a variable
        string letter;

        if (grade >= 90)
            letter = "A";
        else if (grade >= 80)
            letter = "B";
        else if (grade >= 70)
            letter = "C";
        else if (grade >= 60)
            letter = "D";
        else
            letter = "F";

        // Stretch Challenge 1: determine "+" or "-" sign
        string sign = "";

        if (letter != "F")
        {
            int lastDigit = (int)grade % 10;

            if (lastDigit >= 7)
                sign = "+";
            else if (lastDigit < 3)
                sign = "-";
            else
                sign = "";

            // Stretch Challenge 2: no A+ grade
            if (letter == "A" && sign == "+")
                sign = "";
        }
        // Stretch Challenge 3: no F+ or F- grades (sign stays "" for F)

        Console.WriteLine($"Your letter grade is: {letter}{sign}");

        // Core Requirement 2: pass/fail message (must have at least 70 to pass)
        if (grade >= 70)
            Console.WriteLine("Congratulations, you passed the course!");
        else
            Console.WriteLine("You did not pass, but keep working hard — you'll get it next time!");
    }
}