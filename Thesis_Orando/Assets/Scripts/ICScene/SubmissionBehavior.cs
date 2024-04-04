using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmissionBehavior : BasicBehavior
{
    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
        gameObject.SetActive(false);
        WordDragManager.instance.reload.SetActive(false);
        WordDragManager.instance.Submission();
        GameManager.instance.IncrementCurrentSentence();
    }
}
