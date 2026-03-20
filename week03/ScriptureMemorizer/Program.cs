// Scripture Memorizer - CSE 210 Week 3
//
// Exceeding requirements:
// 1. Works with a LIBRARY of scriptures and picks one at random each run.
// 2. Stretch challenge: only hides words that are not already hidden (no wasted turns).
// 3. Clean display with a separator line for readability.

using ScriptureMemorizer;

// --- Scripture Library ---
var library = new List<Scripture>
{
    new Scripture(new Reference("John", 3, 16),
        "For God so loved the world that he gave his only begotten Son that whosoever believeth in him should not perish but have everlasting life"),

    new Scripture(new Reference("Proverbs", 3, 5, 6),
        "Trust in the Lord with all thine heart and lean not unto thine own understanding In all thy ways acknowledge him and he shall direct thy paths"),

    new Scripture(new Reference("2 Nephi", 2, 25),
        "Adam fell that men might be and men are that they might have joy"),

    new Scripture(new Reference("Joshua", 1, 9),
        "Be strong and of a good courage be not afraid neither be thou dismayed for the Lord thy God is with thee whithersoever thou goest"),

    new Scripture(new Reference("Philippians", 4, 13),
        "I can do all things through Christ which strengtheneth me"),
};

// Pick a random scripture
var random = new Random();
var scripture = library[random.Next(library.Count)];

// --- Main Loop ---
while (true)
{
    Console.Clear();
    Console.WriteLine("=== Scripture Memorizer ===\n");
    Console.WriteLine(scripture.GetDisplayText());
    Console.WriteLine();

    if (scripture.IsCompletelyHidden())
    {
        Console.WriteLine("All words are hidden! Great work memorizing!");
        break;
    }

    Console.Write("Press Enter to hide more words, or type 'quit' to exit: ");
    string input = Console.ReadLine() ?? "";

    if (input.Trim().ToLower() == "quit")
        break;

    scripture.HideRandomWords(3);
}