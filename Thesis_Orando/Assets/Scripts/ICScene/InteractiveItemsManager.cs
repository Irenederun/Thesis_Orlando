using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractiveItemsManager : BasicBehavior
{
    public List<GameObject> interactiveIcons;
    public List<Transform> interactiveIconPos;
    //private int clickTimes = 0; 
    static GameObject previouslyClickedItem;

    public List<GameObject> correspondingIcons;

    // public override void ClickedByMouse()
    // {
    //     base.ClickedByMouse();
    //     switch (clickTimes)
    //     {
    //         case 0:
    //             if (previouslyClickedItem != null)
    //             {
    //                 previouslyClickedItem.GetComponent<InteractiveItemsManager>().DeleteIcons();
    //             }
    //
    //             if (!gameObject.name.Contains("UI"))
    //             {
    //                 LoadIcons();
    //                 previouslyClickedItem = gameObject;
    //                 ICManager.instance.itemWithInteractionButtons = gameObject;
    //             }
    //             
    //             print(previouslyClickedItem.name);
    //             
    //             break;
    //         case 1:
    //             DeleteIcons();
    //             break;
    //     }
    // }

    public void SwitchIconAvailability(bool availability)
    {
        switch (availability)
        {
            case true:
                if (previouslyClickedItem != null)
                {
                    previouslyClickedItem.GetComponent<InteractiveItemsManager>().DeleteIcons2();
                }
                LoadIcons2();
                previouslyClickedItem = gameObject;
                ICManager.instance.itemWithInteractionButtons = gameObject;
                break;
            case false:
                DeleteIcons2();
                break;
        }
    }

    public void DeleteIcons2()
    {
        //print(previouslyClickedItem.name + previouslyClickedItem.GetComponent<InteractiveItemsManager>().correspondingIcons.Count);
        
        foreach (GameObject icon in correspondingIcons)
        {
            icon.GetComponent<IconBehaviors>().IconState(false);
        }
        
        previouslyClickedItem = null;
        ICManager.instance.itemWithInteractionButtons = null;
    }
    
    void LoadIcons2()
    {
        foreach (GameObject icon in correspondingIcons)
        {
            icon.GetComponent<IconBehaviors>().IconState(true);
        }
    }

    // public void LoadIcons()
    // {
    //     for (int i = 0; i < interactiveIcons.Count; i++)
    //     {
    //         GameObject thisIcon = Instantiate(interactiveIcons[i]);
    //         thisIcon.transform.parent = gameObject.transform.GetChild(0);
    //         thisIcon.transform.position = interactiveIconPos[i].position;
    //         clickTimes = 1;
    //     }
    // }

    // public void DeleteIcons()
    // {
    //     Destroy(gameObject.transform.GetChild(0).GetChild(0).gameObject);
    //     Destroy(gameObject.transform.GetChild(0).GetChild(1).gameObject);
    //     clickTimes = 0;
    //     previouslyClickedItem = null;
    //     ICManager.instance.itemWithInteractionButtons = null;
    // }

    public void DeleteExistingIcons()
    {
        // if (previouslyClickedItem != null)
        // {
        //     Destroy(previouslyClickedItem.transform.GetChild(0).GetChild(0).gameObject);
        //     Destroy(previouslyClickedItem.transform.GetChild(0).GetChild(1).gameObject);
        //     previouslyClickedItem.GetComponent<InteractiveItemsManager>().clickTimes = 0;
        //     previouslyClickedItem = null;
        //     ICManager.instance.itemWithInteractionButtons = null;
        // }

        if (previouslyClickedItem != null)
        {
            previouslyClickedItem.GetComponent<InteractiveItemsManager>().DeleteIcons2();
        }
    }
}
