using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordDragPlay3 : WordDragDetails
{
    public override void DragComplete(string wordd, string desNamee)
    {
        base.DragComplete(wordd, desNamee);

        if (desNamee.Contains("Pronoun1"))
        {
            wordPosition = 0;
            string word3A = WordDragManager.instance.FindConjugation(wordd, "3A");
            if (word3A.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word3A;
            RecordWords(wordPosition, word3A);
        }
        else if (desNamee.Contains("Adjective"))
        {
            wordPosition = 2;
            string word3B = WordDragManager.instance.FindConjugation(wordd, "3B");
            if (word3B.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word3B;
            RecordWords(wordPosition, word3B);
            
        }
        else if (desNamee.Contains("Pronoun2"))
        {
            wordPosition = 4;
            string word3C = WordDragManager.instance.FindConjugation(wordd, "3A").ToLower();
            if (word3C.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word3C;
            RecordWords(wordPosition, word3C);
            
            DialogueManager.instance.SetResponseVariable
                ("pronounB", word3C);
        }
        else if (desNamee.Contains("Verb"))
        {
            wordPosition = 6;
            string word3D = WordDragManager.instance.FindConjugation(wordd, "3C");
            if (word3D.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word3D + " ";
            RecordWords(wordPosition, word3D);
            
            string verbReverse = WordDragManager.instance.FindConjugation(wordd, "3C_R");
            
            DialogueManager.instance.SetResponseVariable
                ("verbReverse", verbReverse);
        }
        else if (desNamee.Contains("Pronoun3"))
        {
            wordPosition = 7;
            string word3E = WordDragManager.instance.FindConjugation(wordd, "3D");
            if (word3E.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word3E;
            RecordWords(wordPosition, word3E);
            
            DialogueManager.instance.SetResponseVariable
                ("pronounC", word3E);
        }
    }

    public void RecordWords(int pos, string text)
    {
        WordDragManager.instance.AddToSentence(text, pos);
    }
}
