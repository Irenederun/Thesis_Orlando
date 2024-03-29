using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.Serialization;

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
            //Time.timeScale = 1f;
        }
    }

    IEnumerator OnSceneStart()
    {
        yield return new WaitForSeconds(1);
        myFlowchart.SendFungusMessage("OrlandoStarts");
    }

    public void TriggerDialogueOOC(string correspondingMessage)
    {
        myFlowchart.SendFungusMessage(correspondingMessage);

        if (correspondingMessage.Contains("printSentence")) submitTimes++;
    }

    public void SetSentenceVariable(string saidSentence)
    {
        //print(saidSentence);
        myFlowchart.SetStringVariable("saidSentence", saidSentence);
    }

    public void SetResponseVariable(string secondWord)
    {
        myFlowchart.SetStringVariable("secondWord", secondWord);
    }

    // public void SetResponseVariable(string corResponse)
    // {
    //     //print(corResponse);
    //     myFlowchart.SetStringVariable("response", corResponse);
    // }
}
