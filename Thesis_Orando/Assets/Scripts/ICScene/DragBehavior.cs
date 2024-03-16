using System.Collections.Generic;
using UnityEngine;

public class DragBehavior : MonoBehaviour
{
    private bool dragging = false;
    public List<GameObject> availableDesPosHolders;
    private Vector3 originalPos;

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
                gameObject.transform.position = hit.collider.gameObject.transform.position;
                gameObject.GetComponent<Collider2D>().enabled = false;
                gameObject.GetComponent<WordDragDetails>().DragComplete(gameObject.name, hit.collider.gameObject.name);
                
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
