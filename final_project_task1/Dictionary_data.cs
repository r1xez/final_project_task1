using System;
using System.Collections.Generic;

public class DictionaryData
{
    public string Name { get; set; }
    public string Type { get; set; }  // Dictionary type (e.g., "Ukr-Eng")
    public Dictionary<string, List<string>> Words { get; set; } = new();

    public void AddWord(string word, string translation)
    {
        if (!Words.ContainsKey(word))
        {
            Words[word] = new List<string>();
        }
        if (!Words[word].Contains(translation))
        {
            Words[word].Add(translation);
        }
    }

    public void ReplaceWord(string oldWord, string newWord)
    {
        if (Words.ContainsKey(oldWord))
        {
            Words[newWord] = Words[oldWord];
            Words.Remove(oldWord);
        }
    }

    public void ReplaceTranslation(string word, string oldTranslation, string newTranslation)
    {
        if (Words.ContainsKey(word) && Words[word].Contains(oldTranslation))
        {
            int index = Words[word].IndexOf(oldTranslation);
            Words[word][index] = newTranslation;
        }
    }

    public void RemoveWord(string word)
    {
        Words.Remove(word);
    }

    public void RemoveTranslation(string word, string translation)
    {
        if (Words.ContainsKey(word))
        {
            Words[word].Remove(translation);
            if (Words[word].Count == 0)
            {
                Words.Remove(word);
            }
        }
    }

    public List<string> FindTranslation(string word)
    {
        return Words.ContainsKey(word) ? Words[word] : new List<string> { "Translation not found" };
    }
}
