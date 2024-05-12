using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntirePassageReplace : MonoBehaviour
{
    public TextMeshPro text;
    public List<string> originals;

    public void Replace(int originalPos, string newString)
    {
        text.text = text.text.Replace(originals[originalPos], "<b>"+newString+"</b> ");
    }
}