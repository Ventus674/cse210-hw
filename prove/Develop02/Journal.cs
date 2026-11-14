using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayEntries()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("Your journal is empty.");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        List<List<string>> journalData =
            new List<List<string>>();

        foreach (Entry entry in _entries)
        {
            journalData.Add(entry.GetData());
        }

        JsonSerializerOptions options =
            new JsonSerializerOptions();

        options.WriteIndented = true;

        string json = JsonSerializer.Serialize(
            journalData,
            options
        );

        File.WriteAllText(filename, json);

        Console.WriteLine("Journal saved successfully!");
    }

    public void LoadFromFile(string filename)
    {
        string json = File.ReadAllText(filename);

        List<List<string>> journalData =
            JsonSerializer.Deserialize<List<List<string>>>(json);

        if (journalData == null)
        {
            throw new InvalidDataException(
                "The journal file is empty or invalid."
            );
        }

        List<Entry> loadedEntries = new List<Entry>();

        foreach (List<string> data in journalData)
        {
            if (data == null || data.Count != 4)
            {
                throw new InvalidDataException(
                    "The journal file contains an invalid entry."
                );
            }

            Entry entry = new Entry(
                data[0],
                data[1],
                data[2],
                data[3]
            );

            loadedEntries.Add(entry);
        }

        _entries = loadedEntries;

        Console.WriteLine("Journal loaded successfully!");
    }

    public void SearchEntries(string keyword)
    {
        bool found = false;

        foreach (Entry entry in _entries)
        {
            bool responseMatch = entry.GetResponse().Contains(
                keyword,
                StringComparison.OrdinalIgnoreCase
            );

            bool promptMatch = entry.GetPrompt().Contains(
                keyword,
                StringComparison.OrdinalIgnoreCase
            );

            bool moodMatch = entry.GetMood().Contains(
                keyword,
                StringComparison.OrdinalIgnoreCase
            );

            if (responseMatch || promptMatch || moodMatch)
            {
                entry.Display();
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No matching entries found.");
        }
    }
}
