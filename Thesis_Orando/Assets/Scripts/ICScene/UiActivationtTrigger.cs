using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiActivationtTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        //if inventory is on, don't do; if word game is on, don't do. 
        /*
        while (ICManager.instance.inventOn || ICManager.instance.cardGameOn)
        {
            return;
        }
        */
        //ICManager.instance.ChangeUIActivation(true);
        transform.parent.gameObject.GetComponent<InteractiveItemsManager>().SwitchIconAvailability(true);
    }
    
    //only needed since trigger exit somehow happens when walking inside it 
    //  private void OnTriggerStay2D(Collider2D other)
    // {
    //     while (ICManager.instance.inventOn || ICManager.instance.cardGameOn)
    //     {
    //         return;
    //     }
    //     
    //     if (MouseRayCast.instance.hitCheckUI)
    //     {
    //         transform.parent.gameObject.GetComponent<InteractiveItemsManager>().SwitchIconAvailability(true);
    //     }
    // }

    private void OnTriggerExit2D(Collider2D other)
    {
        //ICManager.instance.ChangeUIActivation(false);

        transform.parent.gameObject.GetComponent<InteractiveItemsManager>().SwitchIconAvailability(false);
        //why the fuck this is also happening when actress is walking inside the trigger???
    }
}
