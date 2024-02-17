using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Mouse : MonoBehaviour
{   
    public static Mouse instance;
    public float mouseClickPosX;
    private bool CDOn = false;
    public ActressController actressController;
    private bool stopMouse = false;
    private GameObject dragObj;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Vector2 mousePos;
    private GameObject clickingObj;
    
    void Update()
    {
        if (DialogueManager.instance != null)
        {
            if (DialogueManager.instance.isTalking)
            {
                return;
            }
        }
        
        if (stopMouse)
        {
            return;
        }
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Interactable"));
        RaycastHit2D hitDrag = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Draggable"));
        
        if (Input.GetMouseButtonDown(0))
        {
            if (/*hit != null &&*/ hit.collider != null)
            {
                clickingObj = hit.collider.gameObject;
                clickingObj.GetComponent<BasicBehavior>().ClickedByMouse();
            }
            else
            {
                clickingObj = null;
                if (mousePos.y < -2)
                {
                    if (actressController != null)
                    {
                        if (!CDOn)
                        {
                            MoveActress();
                            CDOn = true;
                            StartCoroutine(ChangeCDState());
                        }
                        
                    }
                }
            }
            
            if (hitDrag.collider != null)
            {
                dragObj = hitDrag.collider.gameObject;
                dragObj.GetComponent<DragBehavior>().OnDragStarting();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (dragObj != null)
            {
                dragObj.GetComponent<DragBehavior>().OnDragExit();
                dragObj = null;
            }
        }
    }

    IEnumerator ChangeCDState()
    {
        yield return new WaitForSeconds(1);
        CDOn = false;
    }

    void MoveActress()
    {
        mouseClickPosX = mousePos.x;
        actressController.LerpToPos(mouseClickPosX);
        ICManager.instance.ActressWalkingMisc(mouseClickPosX);
    }

    public void ChangeMouseInteraction(bool condition)
    {
        stopMouse = condition;
    }
}
