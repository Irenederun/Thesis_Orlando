using System.Collections.Generic;
using UnityEngine;

public class ParallexScrolling : MonoBehaviour
{
    // [SerializeField] private float moveSpeed;
    // [SerializeField] private bool scrollLeft;
    // [SerializeField] private bool isMoving = false;
    // private float singleTextureWidth;
    //
    // // Start is called before the first frame update
    // void Start()
    // {
    //     SetUpTexture();
    //     if (scrollLeft) moveSpeed = -moveSpeed;
    // }
    //
    // void SetUpTexture()
    // {
    //     Sprite sprite = GetComponent<SpriteRenderer>().sprite;
    //     singleTextureWidth = sprite.texture.width / sprite.pixelsPerUnit;
    // }
    //
    // void Scroll()
    // {
    //     float delta = moveSpeed * Time.deltaTime;
    //     transform.position += new Vector3(delta, 0f, 0f);
    // }
    //
    // void CheckReset()
    // {
    //     if (Mathf.Abs((transform.position.x) - singleTextureWidth) > 0)
    //     {
    //         transform.position = new Vector3(0.0f, transform.position.y, transform.position.z);
    //     }
    // }
    //
    // // Update is called once per frame
    // void Update()
    // {
    //     if (isMoving)
    //     {
    //         Scroll();
    //         //CheckReset();
    //     }
    // }
    //
    // public void changeMovingBool(bool moving, bool isLeft)
    // {
    //     isMoving = moving;
    //     scrollLeft = isLeft;
    // }
    
    [System.Serializable]
    public struct Layer
    {
        public float offsetPercentage;
        public List<Transform> objs;
    }
    public List<Layer> parallexRules = new List<Layer>();


    public void UpdatePositions(float delta)
    {
        foreach (Layer rule in parallexRules)
        {
            foreach (Transform obj in rule.objs)
            {
                moveObj(obj, rule.offsetPercentage, delta);
            }
        }
    }

    private void moveObj(Transform obj, float percentage, float delta)
    {
        obj.position += Vector3.right * delta;
        obj.position += Vector3.left * delta * percentage;
    }
}
