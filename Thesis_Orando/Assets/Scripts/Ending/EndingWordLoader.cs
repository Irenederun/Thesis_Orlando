using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingWordLoader : MonoBehaviour
{
    public List<GameObject> words;
    public int listSize;
    public float fontSize;
    public EndingPlayerPiecesLoader playerScript;

    void Start()
    {
        Init();
    }

    void Init()
    {
        for (int i = 0; i < words.Count; i++)
        {
            if (i < GameManager.instance.allMyWords.Count)
            {
                words[i].SetActive(true);
                words[i].GetComponentInChildren<TMP_Text>().text = GameManager.instance.allMyWords[i];
                words[i].name = GameManager.instance.allMyWords[i];
                words[i].GetComponentInChildren<TMP_Text>().fontSize = fontSize;
                words[i].GetComponent<Collider2D>().enabled = true;
            }
            else
            {
                words[i].SetActive(false);
            }
        }
    }
}
