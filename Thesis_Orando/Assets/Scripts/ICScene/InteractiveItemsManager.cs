using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractiveItemsManager : BasicBehavior
{
    public List<GameObject> interactiveIcons;
    public List<Transform> interactiveIconPos;
    private int clickTimes = 0; 
    static GameObject previouslyClickedItem;

    public override void ClickedByMouse()
    {
        base.ClickedByMouse();
        
        switch (clickTimes)
        {
            case 0:
                LoadIcons();
                if (previouslyClickedItem != null)
                {
                    previouslyClickedItem.GetComponent<InteractiveItemsManager>().DeleteIcons();
                }
                previouslyClickedItem = gameObject;
                break;
            case 1:
                DeleteIcons();
                previouslyClickedItem = null;
                break;
        }
    }

    public void LoadIcons()
    {
        for (int i = 0; i < interactiveIcons.Count; i++)
        {
            GameObject thisIcon = Instantiate(interactiveIcons[i]);
            thisIcon.transform.parent = gameObject.transform.GetChild(0);
            thisIcon.transform.position = interactiveIconPos[i].position;
            clickTimes = 1;
        }
    }

    public void DeleteIcons()
    {
        Destroy(gameObject.transform.GetChild(0).GetChild(0).gameObject);
        Destroy(gameObject.transform.GetChild(0).GetChild(1).gameObject);
        clickTimes = 0;
    }
}
