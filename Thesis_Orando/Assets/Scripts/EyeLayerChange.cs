using UnityEngine;

public class EyeLayerChange : MonoBehaviour
{
    public GameObject eyeball;
    
    public void MoveLayerDown()
    {
        GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        if (eyeball != null)
        {
            eyeball.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        }
    }

    public void MoveLayerUp()
    {
        GetComponent<SpriteRenderer>().sortingLayerName = "OverlayOverlay";
        if (eyeball != null)
        {
            eyeball.GetComponent<SpriteRenderer>().sortingLayerName = "OverlayOverlay";
        }
    }
}
