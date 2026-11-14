using System;
using System.Collections.Generic;
using System.Threading;

public class ListingActivity : Activity
{
    private List<string> _prompts;
    private List<string> _unusedPrompts;
    private Random _random;
    private int _itemCount;

    public ListingActivity() : base(
        "Listing",
        "This activity will help you reflect on the good things " +
        "in your life by having you list as many things as you " +
        "can in a certain area.")
    {
        _random = new Random();
        _itemCount = 0;

        _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        _unusedPrompts = new List<string>(_prompts);
    }

    private string GetRandomPrompt()
    {
        if (_unusedPrompts.Count == 0)
        {
            _unusedPrompts = new List<string>(_prompts);
        }

        int index = _random.Next(_unusedPrompts.Count);
        string prompt = _unusedPrompts[index];
        _unusedPrompts.RemoveAt(index);

        return prompt;
    }

    private void CollectItems()
    {
        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
        string currentItem = "";

        Console.Write("> ");

        while (DateTime.Now < endTime)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();

                    if (!string.IsNullOrWhiteSpace(currentItem))
                    {
                        _itemCount++;
                    }

                    currentItem = "";

                    if (DateTime.Now < endTime)
                    {
                        Console.Write("> ");
                    }
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (currentItem.Length > 0)
                    {
                        currentItem = currentItem.Substring(
                            0, currentItem.Length - 1);

                        Console.Write("\b \b");
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    currentItem += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
            else
            {
                Thread.Sleep(25);
            }
        }

        Console.WriteLine();
    }

    public void Run()
    {
        DisplayStartingMessage();

        _itemCount = 0;

        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {GetRandomPrompt()} ---");
        Console.WriteLine();

        Console.WriteLine("You may begin in:");
        ShowCountdown(5);

        Console.WriteLine("Start listing your answers!");
        Console.WriteLine("Press Enter after each item.");
        Console.WriteLine();

        CollectItems();

        Console.WriteLine();
        Console.WriteLine($"You listed {_itemCount} items!");

        DisplayEndingMessage();
    }
}
