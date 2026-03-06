using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        string playAgain = "yes";

        // Stretch Challenge 2: loop to play again
        while (playAgain == "yes")
        {
            // Core Requirement 3: generate random magic number 1-100
            int magicNumber = random.Next(1, 101);

            int guess = 0;
            int guessCount = 0; // Stretch Challenge 1: track guesses

            // Core Requirement 2: loop until correct guess
            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                guessCount++;

                // Core Requirement 1: higher/lower/correct feedback
                if (guess < magicNumber)
                    Console.WriteLine("Higher");
                else if (guess > magicNumber)
                    Console.WriteLine("Lower");
                else
                    Console.WriteLine("You guessed it!");
            }

            // Stretch Challenge 1: display number of guesses
            Console.WriteLine($"It took you {guessCount} guess{(guessCount == 1 ? "" : "es")}.");

            // Stretch Challenge 2: ask to play again
            Console.Write("Do you want to play again? ");
            playAgain = Console.ReadLine().ToLower().Trim();
        }

        Console.WriteLine("Thanks for playing!");
    }
}