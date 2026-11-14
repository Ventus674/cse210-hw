using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        Random random = new Random();

        List<string> prompts = new List<string>
        {
            "What was the best part of my day?",
            "Who was the most interesting person I met today?",
            "What made me smile today?",
            "What was the biggest challenge I faced today?",
            "What is something I am grateful for today?",
            "What is one thing I learned today?",
            "What could I have done better today?",
            "What am I looking forward to tomorrow?"
        };

        int choice = 0;

        Console.WriteLine("Welcome to the Journal Program!");

        while (choice != 6)
        {
            Console.WriteLine();
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Search");
            Console.WriteLine("6. Quit");
            Console.Write("What would you like to do? ");

            string input = Console.ReadLine();

            if (!int.TryParse(input, out choice))
            {
                Console.WriteLine("Please enter a valid number.");
                continue;
            }

            if (choice == 1)
            {
                int index = random.Next(prompts.Count);
                string prompt = prompts[index];

                Console.WriteLine(prompt);
                Console.Write("> ");
                string response = Console.ReadLine();

                Console.Write("How are you feeling today? ");
                string mood = Console.ReadLine();

                string date = DateTime.Now.ToShortDateString();

                Entry entry = new Entry(
                    date,
                    prompt,
                    response,
                    mood
                );

                journal.AddEntry(entry);

                Console.WriteLine("Entry added successfully!");
            }
            else if (choice == 2)
            {
                journal.DisplayEntries();
            }
            else if (choice == 3)
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine();

                try
                {
                    journal.LoadFromFile(filename);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "Error loading journal: " + ex.Message
                    );
                }
            }
            else if (choice == 4)
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine();

                try
                {
                    journal.SaveToFile(filename);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        "Error saving journal: " + ex.Message
                    );
                }
            }
            else if (choice == 5)
            {
                Console.Write("Enter a word to search for: ");
                string keyword = Console.ReadLine();

                journal.SearchEntries(keyword);
            }
            else if (choice == 6)
            {
                Console.WriteLine(
                    "Thank you for using the Journal Program!"
                );
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
}
