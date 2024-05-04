using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndingParagraphLoader : MonoBehaviour
{
    public List<GameObject> listOfSpaces = new List<GameObject>();
    public EndingPlayerPiecesLoader playerScript;
    public EndingWordLoader wordScript;
    private bool ended = false;
    public float speed = 1.0f;
    
    private enum MyState
    {
        Wait,
        Move,
    }
    private MyState myState;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        myState = MyState.Wait;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(6.5f);
        myState = MyState.Move;
    }

    private void Init()
    {
        // for (int i = 0; i < GameManager.instance.SavedBodyParts.Count; i++)
        // {
        //     if (GameManager.instance.SavedBodyParts[i].used)
        //     {
        //         listOfSpaces[i].transform.GetChild(0).gameObject.SetActive(false);
        //         listOfSpaces[i].transform.GetChild(1).gameObject.SetActive(true);
        //     }
        //     else
        //     {
        //         listOfSpaces[i].transform.GetChild(1).gameObject.SetActive(false);
        //         listOfSpaces[i].transform.GetChild(0).gameObject.SetActive(true);
        //     }
        // } 
        
        for (int i = 0; i < listOfSpaces.Count; i++)
        {
            if (i < GameManager.instance.allMyWords.Count)
            {
                listOfSpaces[i].transform.GetChild(0).gameObject.SetActive(false);
                listOfSpaces[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                listOfSpaces[i].transform.GetChild(1).gameObject.SetActive(false);
                listOfSpaces[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
    
    void Update()
    {
        if (myState == MyState.Wait) return;
        
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (transform.position.y >= 57)
        {
            if (!ended)
            {
                ended = true;
                playerScript.PiecesFadeOut();
                //wordscript: fade words out
                wordScript.FadeWordOut();
                StartCoroutine(TextFadeWait());
            }
        }
    }

    IEnumerator TextFadeWait()
    {
        yield return new WaitForSeconds(3);
        playerScript.PlayEndingClapping();
        yield return new WaitForSeconds(3);
        playerScript.TextFadeIn();
    }

    public void ForceStop()
    {
        myState = MyState.Wait;
    }

    public void AllowMove()
    {
        myState = MyState.Move;
    }
}
