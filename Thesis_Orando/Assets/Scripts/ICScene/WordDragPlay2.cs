public class WordDragPlay2 : WordDragDetails
{
    public override void DragComplete(string wordd, string desNamee)
    {
        base.DragComplete(wordd, desNamee);

        if (desNamee.Contains("nounA"))
        {
            wordPosition = 1;
            string word2A = WordDragManager.instance.FindConjugation(wordd, "2A");
            string word2APerson = WordDragManager.instance.FindConjugation(wordd, "2APerson");
            text.text = word2A;
            RecordWords(wordPosition, word2A);
            DialogueManager.instance.SetResponseVariable("nounAPersonForm", word2APerson);
        }
        else if (desNamee.Contains("verbB"))
        {
            wordPosition = 1;
            string word2B = WordDragManager.instance.FindConjugation(wordd, "2B");
            if (word2B.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word2B;
            RecordWords(wordPosition, word2B);
            DialogueManager.instance.SetResponseVariable("usedVerbB", word2B);
        }
        else if (desNamee.Contains("adjectiveA"))
        {
            wordPosition = 3;
            string word2C = WordDragManager.instance.FindConjugation(wordd, "2C");
            if (word2C.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word2C;
            RecordWords(wordPosition, word2C);
            DialogueManager.instance.SetResponseVariable("usedAdjectiveA", word2C);
        }
        else if (desNamee.Contains("nounB"))
        {
            wordPosition = 1;
            string word2D = WordDragManager.instance.FindConjugation(wordd, "2D");
            if (word2D.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word2D;
            RecordWords(wordPosition, word2D);
            DialogueManager.instance.SetResponseVariable("usedNounB", word2D);
            WordDragManager.instance.SetDeterminedWords("usedNounB", word2D);
        }
        else if (desNamee.Contains("verbCFancy"))
        {
            wordPosition = 3;
            string word2E = WordDragManager.instance.FindConjugation(wordd, "fancyVerb");
            if (word2E.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word2E;
            RecordWords(wordPosition, word2E);
            
            WordDragManager.instance.SetDeterminedWords("usedVerbC",wordd.ToLower());
            DialogueManager.instance.SetResponseVariable("usedVerbC", wordd.ToLower());
        }
        else if (desNamee.Contains("verbE"))
        {
            wordPosition = 1;
            string word2F = WordDragManager.instance.FindConjugation(wordd, "2B");
            if (word2F.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word2F;
            RecordWords(wordPosition, word2F);
            DialogueManager.instance.SetResponseVariable("usedVerbE", word2F);
        }
        else if (desNamee.Contains("verbF"))
        {
            wordPosition = 3;
            string word2G = WordDragManager.instance.FindConjugation(wordd, "2B");
            string nounOfVerbF = WordDragManager.instance.FindConjugation(wordd, "2A");
            if (word2G.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word2G;
            RecordWords(wordPosition, word2G);
            DialogueManager.instance.SetResponseVariable("nounOfVerbF", nounOfVerbF);
        }
    }

    public void RecordWords(int pos, string text)
    {
        WordDragManager.instance.AddToSentence(text, pos);
    }
}

