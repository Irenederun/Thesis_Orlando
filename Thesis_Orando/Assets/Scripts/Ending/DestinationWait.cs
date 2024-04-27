using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationWait : MonoBehaviour
{
    private EndingParagraphLoader paragraphScript;
    private float stopScrollLowBar;

    private void Start()
    {
        paragraphScript = transform.parent.parent.parent.parent.gameObject.GetComponent<EndingParagraphLoader>();
        stopScrollLowBar = GetComponent<EndingWordFloating>().triggerYValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= stopScrollLowBar)
        {
            if (!GetComponent<EndingWordFloating>().isFilled)
            {
                ForceStop();
            }
        }
    }

    private void ForceStop()
    {
        paragraphScript.ForceStop();
    }

    public void AllowMove()
    {
        GetComponent<EndingWordFloating>().isFilled = true;
        paragraphScript.AllowMove();
    }
}
