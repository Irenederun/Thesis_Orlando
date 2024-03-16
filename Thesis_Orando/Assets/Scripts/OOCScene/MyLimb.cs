using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLimb : UIBehavior
{
    public Transform connectionPos;

    public Color selectionColor;

    private Color originalColor;

    private void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    public void Select()
    {
        GetComponent<SpriteRenderer>().color = selectionColor;
    }

    public void Reset()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
    }
}
