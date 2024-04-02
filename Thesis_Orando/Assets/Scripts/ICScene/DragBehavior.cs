using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBehavior : MonoBehaviour
{
    private bool dragging = false;
    public List<GameObject> availableDesPosHolders;
    private Vector3 originalPos;
    private Vector3 originalSize;

    void Start()
    {
        originalPos = gameObject.transform.localPosition;
        originalSize = gameObject.GetComponent<BoxCollider2D>().size;
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
        RaycastHit2D hitReload = Physics2D.Raycast(mousePos, Vector2.zero,
            Mathf.Infinity, LayerMask.GetMask("Dragged"));
        
        if (hit.collider != null)
        {
            if (availableDesPosHolders.Contains(hit.collider.gameObject))
            {
                if (hitReload.collider != null)
                {
                     hitReload.collider.gameObject.GetComponent<WordDragDetails>().ReloadWord();
                     hitReload.collider.gameObject.GetComponent<DragBehavior>().Reload();
                     //print(hitReload.collider.gameObject);
                     GetComponent<WordDragDetails>().ReloadWord();
                     GetComponent<DragBehavior>().Reload();
                }

                gameObject.layer = LayerMask.NameToLayer("Dragged");
                ChangeColliderSize(hit.collider.gameObject.GetComponent<BoxCollider2D>());
                gameObject.transform.position = hit.collider.gameObject.transform.position;
                gameObject.GetComponent<WordDragDetails>().DragComplete(gameObject.name, hit.collider.gameObject.name);
                //gameObject.GetComponent<Collider2D>().enabled = false;
                //hit.collider.gameObject.layer = LayerMask.NameToLayer("Default");
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

    private void ChangeColliderSize(BoxCollider2D desCol)
    {
        Vector2 size;
        size.x = desCol.bounds.size.x / transform.lossyScale.x;
        size.y = desCol.bounds.size.y / transform.lossyScale.y;
        gameObject.GetComponent<BoxCollider2D>().size = size;
    }

    private void DragFailed()
    {
        Reload();
    }

    public void Reload()
    {
        gameObject.transform.localPosition = originalPos;
        gameObject.layer = LayerMask.NameToLayer("Draggable");
        gameObject.GetComponent<BoxCollider2D>().size = originalSize;
        gameObject.GetComponent<WordDragDetails>().ReloadWord();
        gameObject.GetComponent<WordDragDetails>().ResetWords();
    }
    
    // private IEnumerator MoveTowards()
    // {
        // Vector3 startPos = transform.localPosition;
        // float t = 0;
        // float duration = 0.6f;
        // while (gameObject.transform.localPosition != originalPos)
        // {
        //     t += 0.02f / duration;
        //     float curvedT = cubicHermiteSpline(t);
        //     transform.localPosition = Vector3.Lerp(startPos, originalPos, curvedT);
        //     //transform.localPosition = 
        //         //Vector3.MoveTowards(transform.localPosition,originalPos, 10 * Time.deltaTime);
        //     yield return new WaitForSeconds(0.02f);
        // }
    //}

    public void ResetToDraggable()
    {
        gameObject.layer = LayerMask.NameToLayer("Draggable");
    }

    private float cubicHermiteSpline(float t)
    {
        return 1 - (2 * t * t * t - 3 * t * t + 1);
    }
}
