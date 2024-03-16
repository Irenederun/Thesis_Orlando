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

            string word1A = WordDragManager.instance.FindConjugation(wordd, "1A");

            text.text = word1A;
            RecordWords(wordPosition, word1A);
        }
        else if (desNamee.Contains("object"))
        {
            wordPosition = 2;

            string word1B = WordDragManager.instance.FindConjugation(wordd, "1B");
            
            text.text = word1B;
            RecordWords(wordPosition, word1B);
            
            WordDragManager.instance.DetermineResponse(wordd, word1B, "1C");
        }
    }

    public void RecordWords(int pos, string text)
    {
        WordDragManager.instance.AddToSentence(text, pos);
    }
}
