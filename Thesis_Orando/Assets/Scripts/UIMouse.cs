using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMouse : MonoBehaviour
{
    public Vector2 mousePos;
    private GameObject clickingObj;
    private GameObject dragObj;
    public static UIMouse instance;
    public GameObject wordBeingDragged;

    private void OnEnable()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }


    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.instance.isTalking)
        {
            return;
        }
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hitClick = Physics2D.Raycast
            (mousePos, Vector3.back, Mathf.Infinity, LayerMask.GetMask("UIInteract"));
        RaycastHit2D hitUIDrag = Physics2D.Raycast
            (mousePos, Vector3.back, Mathf.Infinity, LayerMask.GetMask("UIDrag"));

        if (Input.GetMouseButtonDown(0))
        {
            if (hitClick.collider != null)
            {
                clickingObj = hitClick.collider.gameObject;
                clickingObj.GetComponent<BasicBehavior>().ClickedByMouse();
            }

            if (hitUIDrag.collider != null)
            {
                dragObj = hitUIDrag.collider.gameObject;
                dragObj.GetComponent<UIDragBehavior>().OnDragStarting();

                if (GameManager.instance.isTutorial)
                {
                    DialogueManager.instance.TriggerDialogue("DragStarted");
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (dragObj != null)
            {
                dragObj.GetComponent<UIDragBehavior>().OnDragExit();
                dragObj = null;
                
                if (GameManager.instance.isTutorial)
                {
                    DialogueManager.instance.TriggerDialogue("ExchangeStarted");
                }
            }
        }
    }
}
