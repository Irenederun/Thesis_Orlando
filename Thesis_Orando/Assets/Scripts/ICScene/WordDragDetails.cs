using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordDragDetails : MonoBehaviour
{
    public TextMeshPro text;
    private int wordPosition;
    public List<string> posCorConjugations;

    public void DragComplete(string wordd, string desNamee)
    {
        if (gameObject.name == "cruel" || gameObject.name == "deceive")
        {
            CruelDeceive(wordd, desNamee);
        }
        
        else if (gameObject.name == "you")
        {
            You(wordd, desNamee);
        }
        
        else if (gameObject.name == "me")
        {
            Me(wordd, desNamee);
        }
    }

    private void CruelDeceive(string word, string desName)
    {
        switch (desName)
        {
            case "Pos1-c/d":
                wordPosition = 0;
                SpeechCardManager.instance.AddToSentence(word, 0);
                break;
            case "Pos3-c/d":
                wordPosition = 1;
                SpeechCardManager.instance.AddToSentence(word, 2);
                
                if (gameObject.name == "cruel")
                {
                    if (SpeechCardManager.instance.sentence[3] == "you")
                    {
                        SpeechCardManager.instance.you.GetComponent<WordDragDetails>().You("you", "Pos4-y/m");
                    }
                    else if (SpeechCardManager.instance.sentence[3] == "me")
                    {
                        SpeechCardManager.instance.me.GetComponent<WordDragDetails>().Me("me", "Pos4-y/m");
                    }
                }

                break;
            default:
                print("something wrong");
                break;
        }
        text.text = posCorConjugations[wordPosition];
    }

    private void You(string word, string desName)
    {
        switch (desName)
        {
            case "Pos2-y/m":
                wordPosition = 0;
                SpeechCardManager.instance.AddToSentence(word, 1);
                break;
            case "Pos4-y/m":
                SpeechCardManager.instance.you = gameObject;
                if (SpeechCardManager.instance.sentence[2] != "cruel")
                {
                    wordPosition = 0;
                }
                else
                {
                    wordPosition = 1;
                }
                SpeechCardManager.instance.AddToSentence(word, 3);
                break;
        }
        text.text = posCorConjugations[wordPosition];
    }
    
    private void Me(string word, string desName)
    {
        switch (desName)
        {
            case "Pos2-y/m":
                wordPosition = 0;
                SpeechCardManager.instance.AddToSentence(word, 1);
                break;
            case "Pos4-y/m":
                SpeechCardManager.instance.me = gameObject;
                if (SpeechCardManager.instance.sentence[2] != "cruel")
                {
                    wordPosition = 0;
                }
                else
                {
                    wordPosition = 1;
                }
                SpeechCardManager.instance.AddToSentence(word, 3);
                break;
        }
        text.text = posCorConjugations[wordPosition];
    }

    public void ResetWords()
    {
        text.text = gameObject.name;
    }
}
