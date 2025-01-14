
public class WordDragDetailsPlay1V3 : WordDragDetails
{
    public override void DragComplete(string wordd, string desNamee)
    {
        base.DragComplete(wordd, desNamee);
        
        wordPosition = 1;

        string word1B = WordDragManager.instance.FindConjugation(wordd, "1B");
            
        text.text = word1B;
        RecordWords(wordPosition, word1B);

        DialogueManager.instance.SetResponseVariable
            ("secondWord", WordDragManager.instance.FindConjugation(wordd, "1A"));
        WordDragManager.instance.responseDeterminant = WordDragManager.instance.FindConjugation(wordd, "1C");
    }

    public void RecordWords(int pos, string text)
    {
        WordDragManager.instance.AddToSentence(text, pos);
    }
}
