using System.Collections;
using UnityEngine;

public class MouseRayCast : MonoBehaviour
{   
    public static MouseRayCast instance;
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
        RaycastHit2D hitClick = Physics2D.Raycast
            (mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Interactable"));
        RaycastHit2D hitDrag = Physics2D.Raycast
            (mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Draggable"));
        RaycastHit2D hitCheckClick = Physics2D.Raycast
            (mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("ClickableAreaMoving"));
        
        
        if (Input.GetMouseButtonDown(0))
        {
            if (hitClick.collider != null)
            {
                clickingObj = hitClick.collider.gameObject;
                clickingObj.GetComponent<BasicBehavior>().ClickedByMouse();
            }
            else /*if (hitClick.collider == null && hitDrag.collider == null)*/
            {
                clickingObj = null;
                if (hitCheckClick.collider != null)
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
                print("dragging" + hitDrag.collider.gameObject.name);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (dragObj != null)
            {
                print("RELEASING");
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
        actressController.StartWalking(mouseClickPosX);
        ICManager.instance.ActressWalkingMisc(mouseClickPosX);
    }

    public void ChangeMouseInteraction(bool condition)
    {
        stopMouse = condition;
    }
}
