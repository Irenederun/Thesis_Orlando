using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconBehaviors : BasicBehavior
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
        if (gameObject.name.Contains("UI"))
        {
            OpenInventory();
            TurnOffOtherIcons();
        }
    }

    void OpenInventory()
    {
        ICManager.instance.TurnOnInventory();
    }

    void TurnOffOtherIcons()
    {
        gameObject.transform.parent.parent.GetComponent<InteractiveItemsManager>().DeleteIcons();
    }
}
