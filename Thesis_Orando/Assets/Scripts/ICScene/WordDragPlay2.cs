public class WordDragPlay2 : WordDragDetails
{
    public override void DragComplete(string wordd, string desNamee)
    {
        base.DragComplete(wordd, desNamee);

        if (desNamee.Contains("nounA"))
        {
            wordPosition = 1;
            string word2A = WordDragManager.instance.FindConjugation(wordd, "noun");
            if (word2A.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word2A;
            RecordWords(wordPosition, word2A);
        }
        else if (desNamee.Contains("verbB"))
        {
            wordPosition = 1;
            string word2B = WordDragManager.instance.FindConjugation(wordd, "verb");
            if (word2B.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word2B;
            RecordWords(wordPosition, word2B);
        }
        else if (desNamee.Contains("adjectiveA"))
        {
            wordPosition = 3;
            string word2C = WordDragManager.instance.FindConjugation(wordd, "adj");
            if (word2C.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word2C;
            RecordWords(wordPosition, word2C);
        }
        else if (desNamee.Contains("nounB"))
        {
            wordPosition = 1;
            string word2D = WordDragManager.instance.FindConjugation(wordd, "noun");
            if (word2D.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word2D;
            RecordWords(wordPosition, word2D);
            
            WordDragManager.instance.usedNounB = wordd;
            print("usedNounB" +  WordDragManager.instance.usedNounB );
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
            
            WordDragManager.instance.usedVerbC = wordd;
        }
        else if (desNamee.Contains("verbE"))
        {
            wordPosition = 3;
            string word2F = WordDragManager.instance.FindConjugation(wordd, "verb");
            if (word2F.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word2F;
            RecordWords(wordPosition, word2F);
        }
        else if (desNamee.Contains("verbF"))
        {
            wordPosition = 3;
            string word2G = WordDragManager.instance.FindConjugation(wordd, "verb");
            if (word2G.Contains("no"))
            {
                GetComponent<WordDragDetails>().ReloadWord();
                GetComponent<DragBehavior>().Reload();
                return;
            }
            text.text = word2G;
            RecordWords(wordPosition, word2G);
        }
    }

    public void RecordWords(int pos, string text)
    {
        WordDragManager.instance.AddToSentence(text, pos);
    }
}

