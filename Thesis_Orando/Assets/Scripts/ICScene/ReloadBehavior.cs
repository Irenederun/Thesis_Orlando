using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBehavior : BasicBehavior
{
    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
        foreach (GameObject word in SpeechCardManager.instance.words)
        {
            word.GetComponent<DragBehavior>().Reload();
        }
        
        gameObject.SetActive(false);
        SpeechCardManager.instance.submit.SetActive(false);
        
                
        for (int i = 0; i < 4; i++)
        {
            SpeechCardManager.instance.sentence[i] = " ";
        }
        
        foreach (GameObject obj in SpeechCardManager.instance.destinations)
        {
            obj.layer = LayerMask.NameToLayer("DragDestinations");
        }
    }
}
