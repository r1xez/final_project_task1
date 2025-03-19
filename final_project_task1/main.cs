using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        DictionaryData currentDictionary = null;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Create a dictionary");
            Console.WriteLine("2. Load a dictionary");
            Console.WriteLine("3. Add a word and translation");
            Console.WriteLine("4. Replace a word or translation");
            Console.WriteLine("5. Delete a word or translation");
            Console.WriteLine("6. Find a translation");
            Console.WriteLine("7. Exit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter dictionary name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter dictionary type (e.g., Ukr-Eng): ");
                    string type = Console.ReadLine();

                    currentDictionary = new DictionaryData { Name = name, Type = type };
                    DictionaryManager.SaveDictionary(currentDictionary);
                    Console.WriteLine($"Dictionary \"{name}\" ({type}) created!");
                    break;
                case "2":
                    Console.WriteLine("Available dictionaries:");
                    var dicts = DictionaryManager.GetDictionaryList();
                    for (int i = 0; i < dicts.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {dicts[i]}");
                    }
                    Console.Write("Select dictionary number: ");
                    int index = int.Parse(Console.ReadLine()) - 1;
                    if (index >= 0 && index < dicts.Count)
                    {
                        currentDictionary = DictionaryManager.LoadDictionary(dicts[index]);
                        Console.WriteLine($"Dictionary \"{currentDictionary.Name}\" ({currentDictionary.Type}) loaded!");
                    }
                    break;
                case "3":
                    if (currentDictionary == null)
                    {
                        Console.WriteLine("Load or create a dictionary first.");
                        break;
                    }
                    Console.Write("Enter a word: ");
                    string word = Console.ReadLine();
                    Console.Write("Enter a translation: ");
                    string translation = Console.ReadLine();
                    currentDictionary.AddWord(word, translation);
                    DictionaryManager.SaveDictionary(currentDictionary);
                    Console.WriteLine("Word added!");
                    break;
                case "4":
                    if (currentDictionary == null)
                    {
                        Console.WriteLine("Load or create a dictionary first.");
                        break;
                    }
                    Console.Write("1. Replace word\n2. Replace translation\nYour choice: ");
                    string option = Console.ReadLine();
                    if (option == "1")
                    {
                        Console.Write("Enter old word: ");
                        string oldWord = Console.ReadLine();
                        Console.Write("Enter new word: ");
                        string newWord = Console.ReadLine();
                        currentDictionary.ReplaceWord(oldWord, newWord);
                    }
                    else if (option == "2")
                    {
                        Console.Write("Enter word: ");
                        string targetWord = Console.ReadLine();
                        Console.Write("Enter old translation: ");
                        string oldTrans = Console.ReadLine();
                        Console.Write("Enter new translation: ");
                        string newTrans = Console.ReadLine();
                        currentDictionary.ReplaceTranslation(targetWord, oldTrans, newTrans);
                    }
                    DictionaryManager.SaveDictionary(currentDictionary);
                    Console.WriteLine("Update successful!");
                    break;
                case "5":
                    if (currentDictionary == null)
                    {
                        Console.WriteLine("Load or create a dictionary first.");
                        break;
                    }
                    Console.Write("1. Delete word\n2. Delete translation\nYour choice: ");
                    string deleteOption = Console.ReadLine();
                    Console.Write("Enter word: ");
                    string delWord = Console.ReadLine();
                    if (deleteOption == "1")
                    {
                        currentDictionary.RemoveWord(delWord);
                    }
                    else if (deleteOption == "2")
                    {
                        Console.Write("Enter translation: ");
                        string delTrans = Console.ReadLine();
                        currentDictionary.RemoveTranslation(delWord, delTrans);
                    }
                    DictionaryManager.SaveDictionary(currentDictionary);
                    Console.WriteLine("Deletion successful!");
                    break;
                case "6":
                    if (currentDictionary == null)
                    {
                        Console.WriteLine("Load or create a dictionary first.");
                        break;
                    }
                    Console.Write("Enter word: ");
                    string searchWord = Console.ReadLine();
                    var translations = currentDictionary.FindTranslation(searchWord);
                    Console.WriteLine("Translations: " + string.Join(", ", translations));
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
