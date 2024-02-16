using System.Collections;
using UnityEngine;

public class Mouse : MonoBehaviour
{   
    public static Mouse instance;
    public float mouseClickPosX;
    private bool CDOn = false;
    public ActressController actressController;
    
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
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Interactable"));
        
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
                if (actressController != null)
                {
                    if (!CDOn)
                    {
                        RecordPosition();
                        CDOn = true;
                        StartCoroutine(CDChangeState());
                    }
                    
                }
            }
        }
    }

    IEnumerator CDChangeState()
    {
        yield return new WaitForSeconds(1);
        CDOn = false;
    }

    void RecordPosition()
    {
        mouseClickPosX = mousePos.x;
        //print(mouseClickPosX);
        actressController.LerpToPos(mouseClickPosX);
    }
}
