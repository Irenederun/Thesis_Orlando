using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyWord : UIBehavior
{
    public Color selectionColor;
    public TMP_Text wordText;
    public LineRenderer lr;

    private Color originalColor;
    private Transform connectedLimb;

    public enum state
    {
        idle, selected, connected
    }

    private state curState;

    private void Start()
    {
        originalColor = wordText.color;
        lr.gameObject.SetActive(false);
        curState = state.idle;
    }

    private void Update()
    {
        if (curState == state.selected)
        {
            //Vector3 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //updateLinePositions(endPos);
        }
        else if (curState == state.connected)
        {
            updateLinePositions(connectedLimb.position);
        }
    }

    public void Select()
    {
        curState = state.selected;
        wordText.color = selectionColor;
        wordText.fontStyle = FontStyles.Underline;
        lr.gameObject.SetActive(false);
    }

    public void Connect(Transform limb)
    {
        curState = state.connected;
        connectedLimb = limb;
        lr.gameObject.SetActive(true);
    }

    public void Reset()
    {
        curState = state.idle;
        wordText.color = originalColor;
        wordText.fontStyle = FontStyles.Normal;
        lr.gameObject.SetActive(false);
    }

    private void updateLinePositions(Vector3 endPos)
    {
        Vector3 startPos = transform.position;
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
