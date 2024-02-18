using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechCardManager : ManagerBehavior
{
    public List<string> sentence;
    public static SpeechCardManager instance;
    
    [HideInInspector]public GameObject you;
    [HideInInspector]public GameObject me;

    [HideInInspector]public List<GameObject> destinations;
    [HideInInspector]public List<GameObject> words;
    public GameObject reload;
    public GameObject submit;

    private string finalSentence;

    private void Awake()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = null;
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
        for (int i = 0; i < 4; i++)
        {
            finalSentence += sentence[i] + " ";
        }
        print(finalSentence);
        CompleteSentence();
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
        
        DialogueManager.instance.TriggerDialogueOOC(finalSentence);
        //ICManager.instance.LoadCompleteSentence(finalSentence);
        DestroySelfOnClose();
    }
    
    public override void DestroySelfOnClose()
    {
        base.DestroySelfOnClose();
        Destroy(gameObject);
    }
}
