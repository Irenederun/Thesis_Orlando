using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ParallexScrolling : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool scrollLeft;
    [SerializeField] private bool isMoving = false;
    private float singleTextureWidth;
    
    // Start is called before the first frame update
    void Start()
    {
        SetUpTexture();
        if (scrollLeft) moveSpeed = -moveSpeed;
    }

    void SetUpTexture()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        singleTextureWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }

    void Scroll()
    {
        float delta = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(delta, 0f, 0f);
    }

    void CheckReset()
    {
        if (Mathf.Abs((transform.position.x) - singleTextureWidth) > 0)
        {
            transform.position = new Vector3(0.0f, transform.position.y, transform.position.z);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Scroll();
            //CheckReset();
        }
    }

    public void changeMovingBool(bool moving, bool isLeft)
    {
        isMoving = moving;
        scrollLeft = isLeft;
    }
}
