using TMPro;
using UnityEngine;

public class WordDragDetails : MonoBehaviour
{
    public TextMeshPro text;
    public int wordPosition;

    public virtual void DragComplete(string wordd, string desNamee)
    {
        //print(wordd + desNamee);
    }

    public virtual void ResetWords()
    {
        text.text = gameObject.name;
    }
}
