using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.U2D.IK;

public class OtherWord : UIBehavior
{
    public Color selectionColor;
    public Gradient selectionLineColor;
    public TMP_Text wordText;
    public LineRenderer lr;
    public Transform limb;
    public Transform connectionPos;

    private Color originalColor;
    private Gradient originalLineColor;

    private void Start()
    {
        originalColor = wordText.color;
        originalLineColor = lr.colorGradient;
    }

    private void Update()
    {
        updateLineRendererPositions();
    }

    public void Select()
    {
        wordText.color = selectionColor;
        wordText.fontStyle = FontStyles.Underline;
        lr.colorGradient = selectionLineColor;
    }

    public void Reset()
    {
        wordText.color = originalColor;
        wordText.fontStyle = FontStyles.Normal;
        lr.colorGradient = originalLineColor;
    }

    private void updateLineRendererPositions()
    {
        Vector3 startPos = wordText.transform.position;
        Vector3 endPos = connectionPos.position;
        startPos.z = 0f;
        endPos.z = 0f;

        int vertCount = 15;
        lr.positionCount = vertCount;
        for (int i = 0; i < vertCount; i++)
        {
            lr.SetPosition(i, Vector3.Lerp(startPos, endPos, i / (float)(vertCount-1)));
        }
    }
}
