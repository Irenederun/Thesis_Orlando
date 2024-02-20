using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DragBehavior : MonoBehaviour
{
    private bool dragging = false;
    public List<GameObject> availableDesPosHolders;
    private Vector3 originalPos;
    public string type;
    
    void Start()
    {
        originalPos = gameObject.transform.localPosition;
    }
    
    void Update()
    {
        if (dragging)
        {
            Vector3 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            gameObject.transform.position = mousePos;
        }
    }

    public void OnDragStarting()
    {
        dragging = true;
        if (ICManager.instance != null)
        {
            ICManager.instance.CamAndActressStop();
        }   
    }

    public void OnDragExit()
    {
        dragging = false;
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 
            Mathf.Infinity, LayerMask.GetMask("DragDestinations"));
        
        if (hit.collider != null)
        {
            if (availableDesPosHolders.Contains(hit.collider.gameObject))
            {
                DragCompleted(hit.collider.gameObject);
                switch (type)
                {
                    case "word":
                        hit.collider.gameObject.layer = LayerMask.NameToLayer("Default");
                        break;
                    case "card":
                        Destroy(hit.collider.gameObject);
                        break;
                }
            }
            else
            {
                DragFailed();
            }
        }
        else
        {
            DragFailed();
        }
    }

    private void DragCompleted(GameObject destination)
    {
        gameObject.transform.position = destination.transform.position;
        gameObject.GetComponent<Collider2D>().enabled = false;
        switch (type)
        {
            case "word":
                gameObject.GetComponent<WordDragDetails>().DragComplete(gameObject.name, destination.name);
                break;
            case "card":
                ICManager.instance.CardUsed();
                break;
        }
    }

    private void DragFailed()
    {
        gameObject.transform.localPosition = originalPos;
    }

    public void Reload()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        DragFailed();
        gameObject.GetComponent<WordDragDetails>().ResetWords();
    }
}
