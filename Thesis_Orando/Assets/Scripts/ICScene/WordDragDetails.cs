using TMPro;
using UnityEngine;

public class WordDragDetails : MonoBehaviour
{
    public TextMeshPro text;
    public int wordPosition;

    public virtual void DragComplete(string wordd, string desNamee)
    {
        
    }

    public virtual void ResetWords()
    {
        text.text = gameObject.name;
    }

    public virtual void ReloadWord()
    {
        if (WordDragManager.instance != null) WordDragManager.instance.OnSlotReload(text.text, wordPosition);
    }
}
