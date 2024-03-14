using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpeechCardManager : ManagerBehavior
{
    public List<string> sentence;
    public static SpeechCardManager instance;
    
    // [HideInInspector]public GameObject you;
    // [HideInInspector]public GameObject me;

    public List<GameObject> destinations;
    public List<GameObject> words;
    public GameObject reload;
    public GameObject submit;
    public List<TextMeshPro> wordList;

    private string finalSentence;

    //public List<string> play1PluralWords;
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
        //ICManager.instance.CardGameSubmission();
    }
    
    private void CompleteSentence()
    {
        // switch (finalSentence)
        // {
        //     case "cruel me deceive you ":
        //         //finalSentence = "Indeed, it was cruel of me to deceive you.";
        //         DialogueManager.instance.TriggerDialogueOOC(finalSentence);
        //         break;
        //     case "cruel you deceive me ":
        //         //finalSentence = "Indeed, it was cruel of you to deceive me.";
        //         break;
        //     case "deceive you cruel me ":
        //         finalSentence = "Indeed, it was deceptive of you to be cruel to me.";
        //         break;
        //     case "deceive me cruel you ":
        //         finalSentence = "Indeed, it was deceptive of me to be cruel to you.";
        //         break;
        // }
        //ICManager.instance.LoadCompleteSentence(finalSentence);
        DialogueManager.instance.SetSentenceVariable(finalSentence + ".");
        DialogueManager.instance.TriggerDialogueOOC("printSentence");
        DialogueManager.instance.TriggerDialogueOOC(responseDeterminant);
        
        //this should also be where we determine if the input is accepted. is there a case that we wouldn't let them pass?
        
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
