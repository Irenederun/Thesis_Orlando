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
    //public bool dragCompleted;

    //whichever dialogue manager (flowchart) gets turned on, the game is using that one.
    //should only be one of these in each scene. 
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
            //Time.timeScale = 0f;
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
    }
}
