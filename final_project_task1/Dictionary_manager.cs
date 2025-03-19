using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class DictionaryManager
{
    private static string folderPath = "Dictionaries";

    static DictionaryManager()
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    public static void SaveDictionary(DictionaryData dictionary)
    {
        string filePath = Path.Combine(folderPath, dictionary.Name + ".txt");
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine($"Type:{dictionary.Type}");  // Save dictionary type
            foreach (var entry in dictionary.Words)
            {
                string translations = string.Join(",", entry.Value);
                writer.WriteLine($"{entry.Key}:{translations}");
            }
        }
    }

    public static DictionaryData LoadDictionary(string name)
    {
        string filePath = Path.Combine(folderPath, name + ".txt");
        if (!File.Exists(filePath)) return null;

        DictionaryData dictionary = new DictionaryData { Name = name };
        bool isFirstLine = true;

        foreach (var line in File.ReadLines(filePath))
        {
            if (isFirstLine)
            {
                if (line.StartsWith("Type:"))
                {
                    dictionary.Type = line.Substring(5);
                    isFirstLine = false;
                    continue;
                }
            }

            string[] parts = line.Split(':');
            if (parts.Length == 2)
            {
                string word = parts[0];
                List<string> translations = parts[1].Split(',').ToList();
                dictionary.Words[word] = translations;
            }
        }
        return dictionary;
    }

    public static List<string> GetDictionaryList()
    {
        return Directory.GetFiles(folderPath, "*.txt")
                        .Select(Path.GetFileNameWithoutExtension)
                        .ToList();
    }
}
