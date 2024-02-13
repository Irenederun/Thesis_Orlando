using System.Collections;
using Fungus;
using UnityEngine;

public class DragManagerFungus : ManagerBehavior
{
    public static DragManagerFungus instance;
    private Fungus.Flowchart myFlowchart;
    public bool dragCompleted;

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
    }

    private void Update()
    {
        if (myFlowchart.GetBooleanVariable("dragCompleted") && !dragCompleted)
        {
            dragCompleted = true;
        }
        else if (!myFlowchart.GetBooleanVariable("dragCompleted") && dragCompleted)
        {
            dragCompleted = false;
        }
    }
}
