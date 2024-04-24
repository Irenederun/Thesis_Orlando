using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndingParagraphLoader : MonoBehaviour
{
    public List<GameObject> listOfSpaces = new List<GameObject>();
    public EndingPlayerPiecesLoader playerScript;
    private bool ended = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
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
    
    
    public float speed = 1.0f;

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (transform.position.y >= 39)
        {
            if (!ended)
            {
                ended = true;
                playerScript.PiecesFadeOut();
            }
        }
    }
    
    
}
