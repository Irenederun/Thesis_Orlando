using UnityEngine;

public class Mouse : MonoBehaviour
{   
    public static Mouse instance;
    
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

    // Update is called once per frame
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
            }
        }
    }
}
