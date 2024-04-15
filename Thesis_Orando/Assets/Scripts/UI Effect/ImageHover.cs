using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageHover : MonoBehaviour
{
    public Sprite normalSprite; // 
    public Sprite hoverSprite; // 
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = normalSprite;
    }

    void OnMouseEnter()
    {
        spriteRenderer.sprite = hoverSprite;
    }

    void OnMouseExit()
    {
        spriteRenderer.sprite = normalSprite;
    }
}
