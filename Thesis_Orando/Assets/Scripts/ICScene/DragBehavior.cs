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

    // Start is called before the first frame update
    void Start()
    {
        originalPos = gameObject.transform.localPosition;
    }

    // Update is called once per frame
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
                print(hit.collider.gameObject.name);
                DragCompleted(hit.collider.gameObject);
                hit.collider.gameObject.layer = LayerMask.NameToLayer("Default");
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
        print(destination.name);
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
        print("F");
        gameObject.transform.localPosition = originalPos;
    }

    public void Reload()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        DragFailed();
        gameObject.GetComponent<WordDragDetails>().ResetWords();
    }
}
