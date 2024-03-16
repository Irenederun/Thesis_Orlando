using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordDragDetailsPlay1Interstitial : WordDragDetails
{
    public override void DragComplete(string wordd, string desNamee)
    {
        base.DragComplete(wordd, desNamee);

        if (desNamee.Contains("Feel"))
        {
            wordPosition = 1;
            text.text = wordd.ToLower();
            RecordWords(wordPosition, wordd.ToLower());
            WordDragManager.instance.interstitialAddedText = wordd;
        }
        
        else if (desNamee.Contains("IAm"))
        {
            wordPosition = 1;
            
            string word1D = WordDragManager.instance.FindConjugation(wordd, "1D");

            text.text = word1D;
            RecordWords(wordPosition, word1D);
            
            WordDragManager.instance.DetermineResponse(wordd, word1D, "1E");
        }
    }

    public void RecordWords(int pos, string text)
    {
        WordDragManager.instance.AddToSentence(text, pos);
    }
}
