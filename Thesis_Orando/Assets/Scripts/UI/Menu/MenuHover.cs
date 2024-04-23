using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MenuHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite hoverSprite; // 鼠标悬停时的图片

    private Image buttonImage;
    private Sprite defaultSprite;
    private TextMeshProUGUI buttonText; // 使用TextMeshProUGUI类型

    void Start()
    {
        buttonImage = GetComponent<Image>();
        defaultSprite = buttonImage.sprite;
        buttonText = GetComponentInChildren<TextMeshProUGUI>(); // 获取按钮中的TextMeshProUGUI组件
        buttonText.gameObject.SetActive(false); // 初始状态下关闭按钮文本
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = hoverSprite; // 切换到悬停状态的图片
        buttonText.gameObject.SetActive(true); // 激活按钮文本
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = defaultSprite; // 恢复到默认状态的图片
        buttonText.gameObject.SetActive(false); // 关闭按钮文本
    }
}