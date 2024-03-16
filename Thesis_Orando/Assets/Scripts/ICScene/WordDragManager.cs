using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WordDragManager : ManagerBehavior
{
    public List<string> sentence;
    public static WordDragManager instance;

    public List<GameObject> destinations;
    public List<GameObject> words;
    public GameObject reload;
    public GameObject submit;
    public List<TextMeshPro> wordList;

    private string finalSentence;
    
    public Dictionary dictionary;
    public string responseDeterminant = default;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadWords();
    }

    private void OnDisable()
    {
        instance = null;
    }

    public void LoadWords()
    {
        for (int i = 0; i < GameManager.instance.wordBank.Count; i++)
        {
            wordList[i].text = GameManager.instance.wordBank[i].FirstCharacterToUpper();
            wordList[i].transform.parent.gameObject.name = GameManager.instance.wordBank[i].FirstCharacterToUpper();
        }
    }

    public void AddToSentence(string word, int position)
    {
        sentence[position] = word;
        
        if (!reload.activeSelf)
        {
            reload.SetActive(true);
        }

        if (!sentence.Contains(" "))
        {
            if (!submit.activeSelf)
            {
                submit.SetActive(true);
            }
        }
    }

    public void DetermineResponse(string word1B)
    {
        responseDeterminant = FindConjugation(word1B, "1C");
    }

    public void Submission()
    {
        int senCount = sentence.Count - 1;
        for (int i = 0; i < senCount; i++)
        {
            finalSentence += sentence[i] + " ";
        }
        
        finalSentence += sentence[senCount];
        
        CompleteSentence();
    }
    
    private void CompleteSentence()
    {
        DialogueManager.instance.SetSentenceVariable(finalSentence + ".");
        DialogueManager.instance.TriggerDialogueOOC("printSentence");
        DialogueManager.instance.TriggerDialogueOOC(responseDeterminant);

        DestroySelfOnClose();
    }

    public void OnPlay1Reload()
    {
        sentence[1] = "nothing to say to";
    }

    public string FindConjugation(string word, string position)
    {
        string conjugation = dictionary.Find(word, position);
        return conjugation;
    }
    
    public override void DestroySelfOnClose()
    {
        base.DestroySelfOnClose();
        Destroy(gameObject);
    }
}
