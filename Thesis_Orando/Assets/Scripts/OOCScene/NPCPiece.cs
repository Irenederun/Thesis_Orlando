using UnityEngine;

public class NPCPiece : MonoBehaviour
{
    public bool used;

    private void Start()
    {
        
    }

    public void GrayOut(Color col)
    {
        used = true;
        
        // animmation coroutine
        GetComponent<SpriteRenderer>().color = col;
    }

    public void SetToColor(Color col)
    {
        GetComponent<SpriteRenderer>().color = col;
    }
}
