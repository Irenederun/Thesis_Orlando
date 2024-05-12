using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UIDragBehavior : MonoBehaviour
{
    private bool dragging = false;
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
        AudioManager.instance.PlayAudio("PickUp", transform.parent.gameObject.GetComponent<AudioSource>());
        GetComponent<OtherWord>().UpdatePos();
        UIMouse.instance.wordBeingDragged = gameObject;
    }

    public void OnDragExit()
    {
        dragging = false;
        UIMouse.instance.wordBeingDragged = null;
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 
            Mathf.Infinity, LayerMask.GetMask("UIDragDest"));
        
        if (hit.collider != null)
        {
            AudioManager.instance.PlayAudio("Drop", transform.parent.gameObject.GetComponent<AudioSource>());
            hit.collider.gameObject.SetActive(false);
            gameObject.layer = LayerMask.NameToLayer("UIDragDest");
            gameObject.transform.position = hit.collider.gameObject.transform.position;
            GameManager.instance.allMyWords.Add(GetComponent<OtherWord>().wordText.text);

            string newWord = transform.GetChild(0).GetComponent<TMP_Text>().text;
            string destWord = hit.collider.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text;
            transform.GetChild(0).GetComponent<TMP_Text>().color = ExchangeGameManager.instance.destColor;
            GetComponent<UIWordBehavior>()._exchangeGame.Exchange(destWord, newWord,GetComponent<UIWordBehavior>());
            
            //if success subscribe
            GetComponent<UIWordBehavior>()._exchangeGame.EndEvent += DeactivateObj;
            
            if (GetComponent<MyWord>() == null)
            {
                gameObject.AddComponent<MyWord>();
            }
        }
        
        else
        {
            DragFailed();
        }
    }

    private void DeactivateObj()
    {
        gameObject.SetActive(false);
    }

    private void DragFailed()
    {
        Reload();
    }

    public void Reload()
    {
        gameObject.transform.localPosition = originalPos;
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

    private float cubicHermiteSpline(float t)
    {
        return 1 - (2 * t * t * t - 3 * t * t + 1);
    }
}
