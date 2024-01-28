using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryIconBehavior : BasicBehavior
{
    public GameObject inventPrefab;

    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
        LoadInventory();
    }

    private void LoadInventory()
    {
        Instantiate(inventPrefab);
        OOCManager.instance.SwitchInteractabilityForAll(false);
    }
}
