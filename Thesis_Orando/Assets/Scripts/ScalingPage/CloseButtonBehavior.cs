using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonBehavior : BasicBehavior
{
    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
        OOCManager.instance.SwitchInteractabilityForAll(true);
        ScalingManager.instance.DestroySelfOnClose();
    }
}
