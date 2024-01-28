using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipsIconBehavior : BasicBehavior
{
    public GameObject scalingPagePrefab;

    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
        GameObject scalingPage = Instantiate(scalingPagePrefab);
        OOCManager.instance.SwitchInteractabilityForAll(false);
    }
}
