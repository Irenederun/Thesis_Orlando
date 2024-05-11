using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnterHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite hoverSprite; // 鼠标悬停时的图片

    private Image buttonImage;
    private Sprite defaultSprite;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        defaultSprite = buttonImage.sprite;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = hoverSprite; // 切换到悬停状态的图片
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = defaultSprite; // 恢复到默认状态的图片
    }
}

