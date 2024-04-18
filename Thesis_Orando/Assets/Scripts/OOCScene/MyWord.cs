using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class MyWord : UIBehavior
{
    public Color selectionColor;
    public TMP_Text wordText;
    public LineRenderer lr;

    private Color originalColor;
    private Transform connectedLimb;

    public Color targetColor = new Vector4(0,0,0,0);
    public float maxDistance = 2f;
    public float minDistance = 0.1f;

    public enum state
    {
        idle, selected, connected
    }

    private state curState;

    private void Start()
    {
        if (wordText == null)
        {
            wordText = GetComponent<OtherWord>().wordText;
            GetComponent<OtherWord>().enabled = false;
        }
        
        originalColor = wordText.color;
        //lr.gameObject.SetActive(false);
        curState = state.idle;
    }

    private void Update()
    {
        // if (curState == state.selected)
        // {
        //     //Vector3 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     //updateLinePositions(endPos);
        // }
        // else if (curState == state.connected)
        // {
        //     updateLinePositions(connectedLimb.position);
        // }
        
        ChangeColor();
    }

    public void Select()
    {
        curState = state.selected;
        wordText.color = selectionColor;
        //wordText.fontStyle = FontStyles.Underline;
        //lr.gameObject.SetActive(false);
    }

    public void Connect(Transform limb)
    {
        curState = state.connected;
        connectedLimb = limb;
        //lr.gameObject.SetActive(true);
    }

    public void Reset()
    {
        curState = state.idle;
        wordText.color = originalColor;
        //wordText.fontStyle = FontStyles.Normal;
        //lr.gameObject.SetActive(false);
    }

    private void updateLinePositions(Vector3 endPos)
    {
        Vector3 startPos = transform.position;
        startPos.z = 0f;
        endPos.z = 0f;

        int vertCount = 15;
        //lr.positionCount = vertCount;
        for (int i = 0; i < vertCount; i++)
        {
            //lr.SetPosition(i, Vector3.Lerp(startPos, endPos, i / (float)(vertCount-1)));
        }
    }

    private void ChangeColor()
    {
        if (UIMouse.instance.wordBeingDragged != null)
        {
            GameObject objB = UIMouse.instance.wordBeingDragged;
            float distance = Vector3.Distance(transform.position, objB.transform.position);
            float yDistance = Mathf.Abs(transform.position.y - objB.transform.position.y);

            if (yDistance <= 0.55f)
            {
                if (distance >= minDistance && distance <= maxDistance )
                {
                    // Calculate the lerp factor based on the distance
                    float lerpFactor = (distance - minDistance) / (maxDistance - minDistance);
                    lerpFactor = 1 - lerpFactor;

                    // Interpolate the color from originalColor to targetColor
                    Color newColor = Color.Lerp(originalColor, targetColor, lerpFactor);

                    // Set the color of the TMP component
                    wordText.color = newColor;
                }
                else if (distance < minDistance)
                {
                    wordText.color = targetColor; // Set to bright red
                }
                else if (distance > maxDistance)
                {
                    // Reset the color to the original color if the distance is greater than the detection distance
                    wordText.color = originalColor;
                }
            }
            else
            {
                wordText.color = originalColor;
            }
        }
        else
        {
            wordText.color = originalColor;
        }
    }
}
