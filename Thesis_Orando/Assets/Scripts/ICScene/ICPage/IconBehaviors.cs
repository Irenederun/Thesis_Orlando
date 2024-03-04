using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IconBehaviors : BasicBehavior
{
    [SerializeField] private GameObject actress;
    [SerializeField] private List<Sprite> sprites;
    private bool canInteract;
    private int clickTimes = 0;

    private void Start()
    {
        if (!gameObject.name.Contains("UI"))
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
            canInteract = false;
        }
        else
        {
            canInteract = true;
        }
    }

    public override void ClickedByMouse()
    {
        
        if (canInteract)
        {
            if (gameObject.name.Contains("UI"))
            {
                switch (clickTimes)
                {
                    case 0:
                        OpenInventory();
                        TurnOffOtherIcons();
                        clickTimes++;
                        break;
                    case 1:
                        CloseInventory();
                        clickTimes--;
                        break;
                }
            }
            else if (gameObject.name.Contains("look"))
            {
                //DialogueManager.instance.TriggerDialogueOOC("ArchdukeLook");
                ICManager.instance.StartExchange();
            }
        }
    }

    void CloseInventory()
    {
        ICManager.instance.TurnOffInventory();
    }
    void OpenInventory()
    {
        ICManager.instance.TurnOnInventory();
    }

    void TurnOffOtherIcons()
    {
        if (gameObject.name.Contains("UI"))
        {
            actress.GetComponent<InteractiveItemsManager>().DeleteExistingIcons();
        }
        else
        {
            gameObject.transform.parent.parent.GetComponent<InteractiveItemsManager>().DeleteIcons2();

        }
    }

    public void IconState(bool state)
    {
        switch (state)
        {
            case true:
                GetComponent<SpriteRenderer>().sprite = sprites[1];
                canInteract = true;
                break;
            case false:
                GetComponent<SpriteRenderer>().sprite = sprites[0];
                canInteract = false;
                break;
        }
    }
}
