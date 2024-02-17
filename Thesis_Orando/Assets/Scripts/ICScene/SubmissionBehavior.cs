using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmissionBehavior : BasicBehavior
{
    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
        gameObject.SetActive(false);
        SpeechCardManager.instance.reload.SetActive(false);
        SpeechCardManager.instance.Submission();
    }
}
