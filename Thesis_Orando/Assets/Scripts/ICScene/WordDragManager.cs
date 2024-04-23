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

    public List<TextMeshPro> exceptionTexts;
    public string usedVerbC;
    public string usedNounB;
    public WordDragLoader loader;

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
                if (s != null) s.SetActive(false);
            }
            listOfSentence[sentenceNo].SetActive(true);
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
        //print("pos rn is " + position);
        
        // if (!reload.activeSelf)
        // {
        //     reload.SetActive(true);
        // }

        if (!sentence.Contains(" ") || !sentence.Contains(""))
        {
            if (!submit.activeSelf)
            {
                submit.SetActive(true);
            }
        }
        
        if (sentence.Contains(" ") || sentence.Contains(""))
        {
            if (submit.activeSelf)
            {
                submit.SetActive(false);
            }
        }
    }

    //public void DetermineResponse(/*string word,*/ string wordConjugated/*, string position*/, string responseCAT)
    //{
        // responseDeterminant = FindConjugation(word, position);
        // if (word.Contains("Myself") )
        // {
        //     DialogueManager.instance.SetResponseVariable("yours");
        // }
        // else if (word.Contains("You"))
        // {
        //     DialogueManager.instance.SetResponseVariable("me");
        // }
        //else
        //{
        //}
        //DialogueManager.instance.SetResponseVariable("secondWord",wordConjugated);
        //responseDeterminant = responseCAT;
    //}

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
        
        if (sentence.Contains(" ") || sentence.Contains(""))
        {
            if (submit.activeSelf)
            {
                submit.SetActive(false);
            }
        }
        
        if (!sentence.Contains(" ") && !sentence.Contains(""))
        {
            if (!submit.activeSelf)
            {
                submit.SetActive(true);
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

    public void SetDeterminedText(int num, string text)
    {
        print("change determination text");
        exceptionTexts[num].text = text;//this is not happening
    }

    public void SetDeterminedWords(string name, string text)
    {
        switch (name)
        {
            case "usedVerbC":
                loader.UsedVerbC(text);
                break;
            case"usedNounB":
                loader.UsedNounB(text);
                break;
        }
    }
}
