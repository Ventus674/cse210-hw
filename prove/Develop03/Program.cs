using System;

class Program
{
    static void Main(string[] args)
    {
        // EXCEEDING REQUIREMENTS:
        // The program only selects words that have not
        // already been hidden. This prevents the same
        // word from being selected multiple times and
        // ensures that each turn hides new words.

        Reference reference = new Reference("Proverbs", 3, 5, 6);

        string text = "Trust in the Lord with all thine heart; " +
                      "and lean not unto thine own understanding. " +
                      "In all thy ways acknowledge him, and he " +
                      "shall direct thy paths.";

        Scripture scripture = new Scripture(reference, text);

        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("All words are hidden!");
                running = false;
            }
            else
            {
                Console.WriteLine("Press enter to continue or type 'quit' to finish:");
                string input = Console.ReadLine();

                if (input != null && input.ToLower() == "quit")
                {
                    running = false;
                }
                else
                {
                    scripture.HideRandomWords(3);
                }
            }
        }
    }
}
