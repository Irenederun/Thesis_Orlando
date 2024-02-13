using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardIconBehavior : BasicBehavior
{
    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
        OOCManager.instance.StartCardRound();
    }
}
