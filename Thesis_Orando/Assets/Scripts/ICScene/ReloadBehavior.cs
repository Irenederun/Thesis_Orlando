using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReloadBehavior : BasicBehavior
{
    public string currentLevel;
    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
                
        // for (int i = 0; i < WordDragManager.instance.sentence.Count; i++)
        // {
        //     WordDragManager.instance.sentence[i] = " ";
        // }

        // switch (currentLevel)
        // {
        //     case "play1":
        //         WordDragManager.instance.OnPlay1Reload();
        //         break;
        //     case "play1-interstitial_1":
        //         break;
        // }

        WordDragManager.instance.OnReload(); //change to reload by slot 

        WordDragManager.instance.responseDeterminant = "default"; //only for determining slot

        foreach (GameObject word in WordDragManager.instance.words) //reload the word on the slot
        {
            word.GetComponent<DragBehavior>().Reload();
        }
        
        gameObject.SetActive(false); //no need
        
        WordDragManager.instance.submit.SetActive(false); //no need

        foreach (GameObject obj in WordDragManager.instance.destinations) //no need
        {
            obj.layer = LayerMask.NameToLayer("DragDestinations");
        }
    }
}
