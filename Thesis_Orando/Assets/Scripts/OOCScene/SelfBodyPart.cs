using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SelfBodyPart : MonoBehaviour
{
    public List<SelfPiece> BodyParts = new List<SelfPiece>();

    private void Start()
    {
        loadMySprites();
    }

    private void loadMySprites()
    {
        // ask gameManager
        List<GameManager.BodyPart> bodyParts = new List<GameManager.BodyPart>();
        bodyParts = GameManager.instance.SavedBodyParts;

        for (int i = 0; i < bodyParts.Count; i++)
        {
            if (bodyParts[i].sprite != null)
            {
                BodyParts[i].SetSprite(bodyParts[i].sprite, bodyParts[i].col);
            }
        }
    }

    public void ReceiveRandomPiece(Sprite sp, Color col)
    {
        // reset used states if all pieces are used
        int usedCounter = 0;
        foreach (SelfPiece piece in BodyParts)
        {
            if (piece.used) usedCounter++;
        }

        if (usedCounter == BodyParts.Count)
        {
            for (int i = 0; i < BodyParts.Count; i++)
            {
                BodyParts[i].used = false;
                GameManager.instance.SavedBodyParts[i].used = false;
            }
        }
        
        int randomPiece = Random.Range(0, BodyParts.Count);
        while (BodyParts[randomPiece].used == true)
        {
            randomPiece = Random.Range(0, BodyParts.Count);
        }
        
        BodyParts[randomPiece].AssignSprite(sp, col);
        GameManager.instance.SaveBodyPart(randomPiece, sp, col);
    }
}
