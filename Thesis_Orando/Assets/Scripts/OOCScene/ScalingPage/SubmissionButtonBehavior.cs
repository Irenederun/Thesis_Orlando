using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmissionButtonBehavior : BasicBehavior
{
    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
        ScalingManager.instance.ChipsSubmission();
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void ChangeColor()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
