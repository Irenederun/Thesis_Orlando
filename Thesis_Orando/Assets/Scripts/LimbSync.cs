using System.Collections.Generic;
using UnityEngine;

public class LimbSync : MonoBehaviour
{
    public SpriteRenderer leftArm;
    public SpriteRenderer rightArm;
    public SpriteRenderer leftLeg;
    public SpriteRenderer rightLeg;

    private void Start()
    {
        Sync();
    }

    public void Sync()
    {
        List<Sprite> limbs = GameManager.instance.GetAllLimbs();
        leftArm.sprite = limbs[0];
        rightArm.sprite = limbs[1];
        leftLeg.sprite = limbs[2];
        rightLeg.sprite = limbs[3];
    }
}
