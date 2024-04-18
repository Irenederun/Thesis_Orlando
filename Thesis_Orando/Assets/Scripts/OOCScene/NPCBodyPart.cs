using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCBodyPart : MonoBehaviour
{
    public string myID;
    public Color grayedColor;
    public List<NPCPiece> BodyParts = new List<NPCPiece>();

    public struct SpColInd
    {
        public Sprite sp;
        public Color col;
        public int ind;
    }
    
    private void Start()
    {
        loadPieceStates();
    }

    // ask otherwordlibrary about my piece states
    private void loadPieceStates()
    {
        List<bool> savedStates = new List<bool>();
        savedStates = OtherWordLibrary.instance.FindPieceStates(myID);

        for (int i = 0; i < savedStates.Count; i++)
        {
            if (savedStates[i] == true)
            {
                BodyParts[i].SetToColor(grayedColor);
                BodyParts[i].used = true;
            }
        }
    }

    public SpColInd LoseRandomPiece()
    {
        int randomPiece = Random.Range(0, BodyParts.Count);
        while (BodyParts[randomPiece].used == true)
        {
            randomPiece = Random.Range(0, BodyParts.Count);
        }

        SpColInd spcol = new SpColInd();
        spcol.sp = BodyParts[randomPiece].GetComponent<SpriteRenderer>().sprite;
        spcol.col = BodyParts[randomPiece].GetComponent<SpriteRenderer>().color;
        spcol.ind = randomPiece;
        return spcol;
    }

    public void GrayOutPiece(int ind)
    {
        BodyParts[ind].GrayOut(grayedColor);
        OtherWordLibrary.instance.ModifyPieceState(myID, ind, true);
    }
}
