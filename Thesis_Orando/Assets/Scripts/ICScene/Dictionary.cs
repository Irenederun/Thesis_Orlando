using System.Collections.Generic;
using Fungus;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/WordDictionary")]
public class Dictionary : ScriptableObject
{
    [System.Serializable]
    public struct Rule
    {
        public string position;
        public string conjugation;
    }
    
    [System.Serializable]
    public struct Word
    {
        public string word;
        public List<Rule> rules;
    }
    public List<Word> words;

    public string Find(string inputWord, string inputPos)
    {
        string outputWord = "init";
        
        for (int i = 0; i < words.Count; i++)
        {
            if (words[i].word == inputWord)
            {
                for (int j = 0; j < words[i].rules.Count; j++)
                {
                    if (words[i].rules[j].position == inputPos)
                    {
                        outputWord = words[i].rules[j].conjugation;
                    }
                }
            }
        }
        return outputWord;
    }
}
