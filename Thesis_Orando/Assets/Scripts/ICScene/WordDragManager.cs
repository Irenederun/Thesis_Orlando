using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WordDragManager : ManagerBehavior
{
    public List<string> sentence;
    public static WordDragManager instance;

    //public List<GameObject> destinations;
    //public List<GameObject> words;
    public GameObject reload;
    public GameObject submit;
    public List<TextMeshPro> wordList;

    private string finalSentence;
    
    public Dictionary dictionary;
    public string responseDeterminant = default;
    public bool doNotLoadFromGM;
    private List<string> initSentence = new List<string>();
    public string interstitialAddedText;
    public bool isInterstitial;

    public List<GameObject> listOfSentence;
    public int sentenceNo;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (submit.activeSelf) submit.SetActive(false);
        if (!doNotLoadFromGM) LoadWords();
        for (int i = 0; i < sentence.Count; i++)
        {
            initSentence.Add(sentence[i]);
        }

        if (!isInterstitial)
        {
            foreach (GameObject s in listOfSentence)
            {
                s.SetActive(false);
            }
            listOfSentence[sentenceNo].SetActive(true);
        }
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
        
        // if (!reload.activeSelf)
        // {
        //     reload.SetActive(true);
        // }

        if (!sentence.Contains(" "))
        {
            if (!submit.activeSelf)
            {
                submit.SetActive(true);
            }
        }
    }

    public void DetermineResponse(string word, string wordConjugated, string position)
    {
        responseDeterminant = FindConjugation(word, position);
        if (wordConjugated.Contains("me") )
        {
            DialogueManager.instance.SetResponseVariable("you");
        }
        else if (wordConjugated.Contains("you"))
        {
            DialogueManager.instance.SetResponseVariable("me");
        }
        else
        {
            DialogueManager.instance.SetResponseVariable(wordConjugated);
        }
    }

    public void Submission()
    {
        int senCount = sentence.Count - 1;
        for (int i = 0; i < senCount; i++)
        {
            finalSentence += sentence[i];
        }
        finalSentence += sentence[senCount];
        
                
        if (doNotLoadFromGM)
        {
            GameManager.instance.wordBank.Add(interstitialAddedText);
        }
        
        CompleteSentence();
    }

    private void CompleteSentence()
    {
        DialogueManager.instance.SetSentenceVariable(finalSentence);
        if (isInterstitial)
        {
            DialogueManager.instance.TriggerDialogue("printSentence" + DialogueManager.instance.submitTimes);
        }
        else
        {
            DialogueManager.instance.TriggerDialogue("printSentence" + GameManager.instance.currentSentenceNo);
        }
        DialogueManager.instance.TriggerDialogue(responseDeterminant);

        DestroySelfOnClose();
    }

    public void OnReload()
    {
        // for (int i = 0; i < sentence.Count; i++)
        // {
        //     sentence[i] = initSentence[i];
        // }
        //
        // interstitialAddedText = "";
    }

    public void OnSlotReload(string word, int slotNo)
    {
        print(word + sentence[slotNo]);
        if (word == sentence[slotNo]) sentence[slotNo] = initSentence[slotNo]; 
        //this also needs to happen if word has changed dest drag box
        
        if (sentence.Contains(" "))
        {
            if (submit.activeSelf)
            {
                submit.SetActive(false);
            }
        }
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
