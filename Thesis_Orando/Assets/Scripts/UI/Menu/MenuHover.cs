using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Sprite hoverSprite; // 鼠标悬停时的图片
    public Sprite clickSprite; // 按钮点击时的图片
    public GameObject creditPage; // Credit页面的UI对象
    public Image fadeImage; // 渐变遮罩图片
    public float fadeDuration = 1.0f; // 渐变持续时间

    private Image buttonImage;
    private Sprite defaultSprite;
    private bool isFading = false; // 是否正在进行渐变

    void Start()
    {
        buttonImage = GetComponent<Image>();
        defaultSprite = buttonImage.sprite;
        creditPage.SetActive(false); // 初始状态下关闭Credit页面
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isFading)
        {
            buttonImage.sprite = hoverSprite; // 切换到悬停状态的图片
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isFading)
        {
            buttonImage.sprite = defaultSprite; // 恢复到默认状态的图片
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndShowCredit());
        }
    }

    IEnumerator FadeAndShowCredit()
    {
        isFading = true;

        // 开始渐变效果（从透明到不透明）
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        // 显示Credit页面
        creditPage.SetActive(true);

        // 结束渐变效果（从不透明到透明）
        timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        isFading = false;
    }
}