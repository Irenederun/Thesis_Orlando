using UnityEngine;

public class MouseRayCast : MonoBehaviour
{   
    public static MouseRayCast instance;
    //public float mouseClickPosX;
    //public ActressController actressController;
    private GameObject dragObj;
    //public bool hitCheckUI;

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

    private Vector3 mousePos;
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

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hitClick = Physics2D.Raycast
            (mousePos, Vector3.back, Mathf.Infinity, LayerMask.GetMask("Interactable"));
        RaycastHit2D hitDrag = Physics2D.Raycast
            (mousePos, Vector3.back, Mathf.Infinity, LayerMask.GetMask("Draggable"));
        //RaycastHit2D hitCheckClick = Physics2D.Raycast
            //(mousePos, Vector3.back, Mathf.Infinity, LayerMask.GetMask("ClickableAreaMoving"));
        RaycastHit2D hitDragged = Physics2D.Raycast
            (mousePos, Vector3.back, Mathf.Infinity, LayerMask.GetMask("Dragged"));

        if (Input.GetMouseButtonDown(0))
        {
            if (hitClick.collider != null)
            {
                clickingObj = hitClick.collider.gameObject;
                clickingObj.GetComponent<BasicBehavior>().ClickedByMouse();
            }
            
            // else /*if (hitClick.collider == null && hitDrag.collider == null)*/
            // {
            //     clickingObj = null;
            //     if (hitCheckClick.collider != null)
            //     {
            //         hitCheckUI = true;
            //         print("hit check");
            //         if (actressController != null)
            //         {
            //              MoveActress();
            //         }
            //     }
            // }
            if (hitDragged.collider != null)
            {
                hitDragged.collider.gameObject.GetComponent<DragBehavior>().ResetToDraggable();
                hitDrag = hitDragged;
            }
            
            if (hitDrag.collider != null)
            {
                //print("dragging" + hitDrag.collider.gameObject.name);
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


            //hitCheckUI = false;
        }
    }

    void MoveActress()
    {
        // mouseClickPosX = mousePos.x;
        // Transform dest = ICManager.instance.ActressWalkingMisc(mouseClickPosX);
        // actressController.StartWalking(dest);
    }
}
