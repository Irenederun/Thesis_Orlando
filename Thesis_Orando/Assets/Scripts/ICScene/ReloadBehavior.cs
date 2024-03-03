using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReloadBehavior : BasicBehavior
{
    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
                
        for (int i = 0; i < SpeechCardManager.instance.sentence.Count; i++)
        {
            SpeechCardManager.instance.sentence[i] = " ";
        }

        SpeechCardManager.instance.onPlay1Reload();
        
        foreach (GameObject word in SpeechCardManager.instance.words)
        {
            word.GetComponent<DragBehavior>().Reload();
        }
        
        gameObject.SetActive(false);
        SpeechCardManager.instance.submit.SetActive(false);

        foreach (GameObject obj in SpeechCardManager.instance.destinations)
        {
            obj.layer = LayerMask.NameToLayer("DragDestinations");
        }
    }
}
