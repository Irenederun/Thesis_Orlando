using System;
using System.Collections.Generic;
using Fungus;
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

    public List<string> play1PluralWords;

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

    public void Submission()
    {
        for (int i = 0; i < sentence.Count; i++)
        {
            finalSentence += sentence[i] + " ";
        }
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
        DestroySelfOnClose();
    }

    public void onPlay1Reload()
    {
        sentence[1] = "nothing to say to";
    }
    
    public override void DestroySelfOnClose()
    {
        base.DestroySelfOnClose();
        Destroy(gameObject);
    }
}
