using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReloadBehavior : BasicBehavior
{
    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
                
        for (int i = 0; i < WordDragManager.instance.sentence.Count; i++)
        {
            WordDragManager.instance.sentence[i] = " ";
        }
        WordDragManager.instance.OnPlay1Reload();

        WordDragManager.instance.responseDeterminant = "default";

        foreach (GameObject word in WordDragManager.instance.words)
        {
            word.GetComponent<DragBehavior>().Reload();
        }
        
        gameObject.SetActive(false);
        WordDragManager.instance.submit.SetActive(false);

        foreach (GameObject obj in WordDragManager.instance.destinations)
        {
            obj.layer = LayerMask.NameToLayer("DragDestinations");
        }
    }
}
