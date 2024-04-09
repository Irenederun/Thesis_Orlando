using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordDragDetailsPlay3Inter : WordDragDetails
{
public override void DragComplete(string wordd, string desNamee)
    {
        base.DragComplete(wordd, desNamee);

        if (desNamee.Contains("1"))
        {
                    
        wordPosition = 1;

        string word2IA = WordDragManager.instance.FindConjugation(wordd, "2IA");

        if (word2IA.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            
        text.text = word2IA;
        RecordWords(wordPosition, word2IA);

        }

        else if (desNamee.Contains("2"))
        {
            wordPosition = 2;

        string word2IB = WordDragManager.instance.FindConjugation(wordd, "2IB");

        
        if (word2IB.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }            


        text.text = word2IB;
        RecordWords(wordPosition, word2IB);

        WordDragManager.instance.responseDeterminant = WordDragManager.instance.FindConjugation(wordd, "2IC");
        }

    }

    public void RecordWords(int pos, string text)
    {
        WordDragManager.instance.AddToSentence(text, pos);
    }
}
