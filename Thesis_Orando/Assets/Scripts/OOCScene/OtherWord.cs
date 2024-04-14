using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public string level;
    public string posInWordList;
    private List<string> outputList;
    public string limbName;
    
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        originalColor = wordText.color;
        originalLineColor = lr.colorGradient;
        //wordText.text = GetComponent<UIWordBehavior>()._exchangeGame.otherWords[posInWordList];
        outputList = OtherWordLibrary.instance.Find(level, posInWordList);
        if (outputList[0] == " ")
        {
            GetComponent<Collider2D>().enabled = false;
        }
        wordText.text = outputList[0];
        limbName = outputList[3];
        limb.gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.GetLimb(limbName);
    }

    private void Update()
    {
        updateLineRendererPositions();
    }

    public void Select()
    {
        wordText.color = selectionColor;
        //wordText.fontStyle = FontStyles.Underline;
        lr.colorGradient = selectionLineColor;
    }

    public void Reset()
    {
        wordText.color = originalColor;
        //wordText.fontStyle = FontStyles.Normal;
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

    public void UpdatePos()
    {
        outputList = OtherWordLibrary.instance.Find(level, posInWordList);
    }

    public void ChangeWordText()
    {
        int i = int.Parse(outputList[1]);
        int j = int.Parse(outputList[2]);
        OtherWordLibrary.instance.Modify(i,j," ", limbName);
    }
}
