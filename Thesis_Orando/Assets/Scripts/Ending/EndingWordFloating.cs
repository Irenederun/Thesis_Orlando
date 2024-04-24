using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndingWordFloating : MonoBehaviour
{
    public bool isFilled = false;
    public EndingWordLoader wordScript;
    public float triggerYValue = 5.0f;
    public static List<GameObject> objectsToSelectFrom = new List<GameObject>();
    private int index;
    public float lerpSpeed = 1.0f;
    private float lerpTimer = 0.0f;
    public EndingPlayerPiecesLoader playerSctipt;

    void Start()
    {
        if (objectsToSelectFrom.Count == 0)
            objectsToSelectFrom.AddRange(wordScript.words.GetRange(0, GameManager.instance.allMyWords.Count));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= triggerYValue)
        {
            if (!isFilled)
            {
                SelectWord();
            }
        }
    }

    public void SelectWord()
    {
        print(objectsToSelectFrom.Count);
        isFilled = true;
        index = Random.Range(0, objectsToSelectFrom.Count);
        GameObject chosenWord = objectsToSelectFrom[index];
        chosenWord.transform.parent = gameObject.transform;
        chosenWord.GetComponent<EndingWordLerping>().MoveWord(transform);
        objectsToSelectFrom.Remove(chosenWord);
        playerSctipt.TurnOffPiece();
    }
}
