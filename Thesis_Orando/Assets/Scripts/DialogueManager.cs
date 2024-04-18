using System.Collections;
using Fungus;
using UnityEngine;

public class DialogueManager : ManagerBehavior
{
    public static DialogueManager instance;
    private Fungus.Flowchart myFlowchart;
    public bool isTalking;
    public int submitTimes;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            instance = null;
            instance = this;
        }
    }

    private void OnDisable()
    {
        instance = null;
    }

    void Start()
    {
        myFlowchart = GetComponent<Flowchart>();
        IEnumerator coroutine = OnSceneStart();
        StartCoroutine(coroutine);
        SetCurrentSentenceVariable();
        ReadLastPlayRating();
    }

    private void Update()
    {
        if (myFlowchart.GetBooleanVariable("isTalking") && !isTalking)
        {
            isTalking = true;
        }
        else if (!myFlowchart.GetBooleanVariable("isTalking") && isTalking)
        {
            isTalking = false;
        }
    }

    IEnumerator OnSceneStart()
    {
        yield return new WaitForSeconds(1);
        myFlowchart.SendFungusMessage("OrlandoStarts");
    }

    public void TriggerDialogue(string correspondingMessage)
    {
        myFlowchart.SendFungusMessage(correspondingMessage);

        if (correspondingMessage.Contains("printSentence")) submitTimes++;
    }

    public void SetSentenceVariable(string saidSentence)
    {
        //print(saidSentence);
        myFlowchart.SetStringVariable("saidSentence", saidSentence);
    }

    public void SetResponseVariable(string variableName, string variableContent)
    {
        myFlowchart.SetStringVariable(variableName, variableContent);
    }

    public void SetCurrentSentenceVariable()
    {   
        myFlowchart.SetIntegerVariable("currentSentence", GameManager.instance.currentSentenceNo);
    }

    // public void SetResponseVariable(string corResponse)
    // {
    //     //print(corResponse);
    //     myFlowchart.SetStringVariable("response", corResponse);
    // }

    public string SetToLower(string word)
    {
        return word.ToLower();
    }

    public void ReadLastPlayRating()
    {
        myFlowchart.SetIntegerVariable("playRating", GameManager.instance.playRating);
    }

    public void IncrementRating()
    {
        GameManager.instance.IncrementPlayRating();
    }

    public void ResetRating()
    {
        GameManager.instance.ResetRating();
    }
}
