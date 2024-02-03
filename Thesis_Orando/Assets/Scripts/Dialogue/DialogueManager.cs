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

    // Start is called before the first frame update
    void Start()
    {
        myFlowchart = GetComponent<Flowchart>();
        IEnumerator coroutine = OnOOCSceneStart();
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        if (myFlowchart.GetBooleanVariable("isTalking"))
        {
            isTalking = true;
            //Time.timeScale = 0f;
        }
        else
        {
            isTalking = false;
            //Time.timeScale = 1f;
        }
    }

    IEnumerator OnOOCSceneStart()
    {
        yield return new WaitForSeconds(1);
        myFlowchart.SendFungusMessage("OrlandoStarts");
    }

    public void TriggerDialogueOOC(string correspondingMessage)
    {
        myFlowchart.SendFungusMessage(correspondingMessage);
    }
}
