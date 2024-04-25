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
        StartCoroutine(Wait());
        foreach (GameObject word in words)
        {
            word.SetActive(false);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        Init();
    }

    void Init()
    {
        for (int i = 0; i < words.Count; i++)
        {
            if (i < GameManager.instance.allMyWords.Count)
            {
                Color col = words[i].GetComponentInChildren<TextMeshPro>().color;
                words[i].GetComponentInChildren<TextMeshPro>().color = new Color(col.r, col.g, col.b, 0);
                words[i].SetActive(true);
                StartCoroutine(FadeWordIn(words[i], 2f));
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

    IEnumerator FadeWordIn(GameObject word, float duration)
    {
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime / duration;

            alpha = Mathf.Clamp01(alpha);

            Color color = word.GetComponentInChildren<TextMeshPro>().color;
            color.a = alpha;
            word.GetComponentInChildren<TextMeshPro>().color = color;
            yield return null;
        }
    }
}
