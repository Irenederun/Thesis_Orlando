using UnityEngine;

public class SelfPiece : MonoBehaviour
{
    public bool used;

    public void AssignSprite(Sprite sp, Color col)
    {
        used = true;
        GetComponent<SpriteRenderer>().sprite = sp;
        GetComponent<SpriteRenderer>().color = col;
        
        //change alpha 0-1 coroutine
    }

    public void SetSprite(Sprite sp, Color col)
    {
        used = true;
        GetComponent<SpriteRenderer>().sprite = sp;
        GetComponent<SpriteRenderer>().color = col;
    }
}
