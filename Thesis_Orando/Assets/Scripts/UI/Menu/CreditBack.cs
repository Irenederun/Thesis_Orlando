using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreditBack : MonoBehaviour, IPointerClickHandler
{
    public GameObject creditPage; // Credit页面的UI对象
    public GameObject Spotlight;
    public Image fadeImage; // 渐变遮罩图片
    public float fadeDuration = 1.0f; // 渐变持续时间

    private bool isFading = false; // 是否正在进行渐变

    void Start()
    {
        creditPage.SetActive(true); // 初始状态下开启Credit页面
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(FadeAndShowMenu());
    }

    IEnumerator FadeAndShowMenu()
    {
        isFading = true;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        creditPage.SetActive(false);

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