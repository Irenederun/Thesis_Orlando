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
    public bool hitCheckUI;
    
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
        
        if (stopMouse)
        {
            return;
        }
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hitClick = Physics2D.Raycast
            (mousePos, Vector3.back, Mathf.Infinity, LayerMask.GetMask("Interactable"));
        RaycastHit2D hitDrag = Physics2D.Raycast
            (mousePos, Vector3.back, Mathf.Infinity, LayerMask.GetMask("Draggable"));
        RaycastHit2D hitCheckClick = Physics2D.Raycast
            (mousePos, Vector3.back, Mathf.Infinity, LayerMask.GetMask("ClickableAreaMoving"));
        //
        // var screenPoint = Input.mousePosition;
        // screenPoint.z = 5.0f; /*distance of the plane from the camera*/
        // mousePos = Camera.main.ScreenToWorldPoint(screenPoint);
        // RaycastHit2D hitClick = 
        //     Physics2D.GetRayIntersection(new Ray(mousePos, Vector3.forward),
        //                 Mathf.Infinity, LayerMask.GetMask("Interactable"));
        // RaycastHit2D hitDrag = 
        //     Physics2D.GetRayIntersection(new Ray(mousePos, Vector3.forward), Mathf.Infinity, 
        //                 LayerMask.GetMask("Draggable"));
        // RaycastHit2D hitCheckClick = 
        //     Physics2D.GetRayIntersection(new Ray(mousePos, Vector3.forward), 
        //                 Mathf.Infinity, LayerMask.GetMask("ClickableAreaMoving"));
        
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
                    hitCheckUI = true;
                    print("hit check");
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
                print("dragging" + hitDrag.collider.gameObject.name);
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

            hitCheckUI = false;
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
        Transform dest = ICManager.instance.ActressWalkingMisc(mouseClickPosX);
        actressController.StartWalking(dest);
    }

    public void ChangeMouseInteraction(bool condition)
    {
        stopMouse = condition;
    }
}
