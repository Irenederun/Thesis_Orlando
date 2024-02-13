using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipsIconBehavior : BasicBehavior
{
    //TODO: currently the icon is used as card submission button

    public override void ClickedByMouse()
    {
        base.ClickedByMouse();

        //these all needs to go to manager
        if (OOCManager.instance.selectedCard != null)
        {
            OOCManager.instance.CardSubmittedOpenScaling();
        }
    }
}
