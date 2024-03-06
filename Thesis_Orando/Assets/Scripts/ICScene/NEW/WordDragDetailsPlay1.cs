using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WordDragDetailsPlay1 : WordDragDetails
{
    public override void DragComplete(string wordd, string desNamee)
    {
        base.DragComplete(wordd, desNamee);

        if (desNamee.Contains("subject"))
        {
            wordPosition = 0;
            
            // if (SpeechCardManager.instance.play1PluralWords.Contains(wordd))
            // {
            //     posCorConjugations[0] = wordd.FirstCharacterToUpper() + " have";
            // }
            // else
            // {
            //     posCorConjugations[0] = wordd.FirstCharacterToUpper() + " has";
            // }

            posCorConjugations[0] = SpeechCardManager.instance.FindConjugation(wordd, "1A");

            text.text = posCorConjugations[0];
            RecordWords(wordPosition, posCorConjugations[0]);
        }
        else if (desNamee.Contains("object"))
        {
            wordPosition = 2;
            
            // posCorConjugations[1] = wordd.ToLower();

            posCorConjugations[1] = SpeechCardManager.instance.FindConjugation(wordd, "1B");
            
            text.text = posCorConjugations[1];
            RecordWords(wordPosition, posCorConjugations[1]);
        }
    }

    public void RecordWords(int pos, string text)
    {
        SpeechCardManager.instance.AddToSentence(text, pos);
    }
}
