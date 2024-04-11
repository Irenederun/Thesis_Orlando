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
            DialogueManager.instance.SetResponseVariable
                ("adjective", word3B);
            WordDragManager.instance.responseDeterminant = WordDragManager.instance.FindConjugation(wordd, "3E");
        }
        else if (desNamee.Contains("Pronoun2"))
        {
            wordPosition = 4;
            string word3C = WordDragManager.instance.FindConjugation(wordd, "3A");
            
            if (!word3C.Contains("I"))
            {
                word3C = word3C.ToLower();
            }
            
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
            string word3D = WordDragManager.instance.FindConjugation(wordd, "3C") + " ";
            if (word3D.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word3D;
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
        else if (desNamee.Contains("verbO"))
        {
            wordPosition = 1;
            string verbO = WordDragManager.instance.FindConjugation(wordd, "verb");
            text.text = verbO;
            RecordWords(wordPosition, verbO);
        }
        else if (desNamee.Contains("nounF"))
        {
            wordPosition = 3;
            string nounF = WordDragManager.instance.FindConjugation(wordd, "noun");
            text.text = nounF;
            RecordWords(wordPosition, nounF);
            DialogueManager.instance.SetResponseVariable("secondWord", nounF);
        }        
        else if (desNamee.Contains("adjK"))
        {
            wordPosition = 1;
            string adjK = WordDragManager.instance.FindConjugation(wordd, "noun");
            text.text = adjK;
            RecordWords(wordPosition, adjK);
        }
        else if (desNamee.Contains("nounG"))
        {
            wordPosition = 3;
            string nounG = WordDragManager.instance.FindConjugation(wordd, "noun");
            text.text = nounG;
            RecordWords(wordPosition, nounG);
        }
        else if (desNamee.Contains("adjM"))
        {
            wordPosition = 5;
            string adjM = WordDragManager.instance.FindConjugation(wordd, "adj");
            string adjMNoun;
            if (wordd.Contains("Self") || wordd.Contains("Rule")||wordd.Contains("You") || wordd.Contains("He") || wordd.Contains("Broken"))
            {
                adjMNoun = WordDragManager.instance.FindConjugation(wordd, "adjMNoun");
            }
            else
            {
                adjMNoun = WordDragManager.instance.FindConjugation(wordd, "adj");
            }
            text.text = adjM;
            RecordWords(wordPosition, adjM);
            DialogueManager.instance.SetResponseVariable("secondWord", adjMNoun);
        }
        else if (desNamee.Contains("nounN"))
        {
            wordPosition = 1;
            string nounN = WordDragManager.instance.FindConjugation(wordd, "noun");
            text.text = nounN;
            RecordWords(wordPosition, nounN);
        }
        else if (desNamee.Contains("nounO"))
        {
            wordPosition = 3;
            string nounO = WordDragManager.instance.FindConjugation(wordd, "noun");
            text.text = nounO;
            RecordWords(wordPosition, nounO);
        }
    }

    public void RecordWords(int pos, string text)
    {
        WordDragManager.instance.AddToSentence(text, pos);
    }
}
