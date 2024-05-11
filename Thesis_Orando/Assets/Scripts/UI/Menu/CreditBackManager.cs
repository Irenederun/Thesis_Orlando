using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditBackManager : MonoBehaviour
{
    public GameObject creditPage; // Credit页面的UI对象
    public Image fadeImage; // 渐变遮罩图片
    public float fadeDuration = 1.0f; // 渐变持续时间

    // 渐变效果的协程
    IEnumerator FadeOut()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        creditPage.SetActive(false); // Credit页面消失

        // 等待一段时间确保Credit页面已经完全消失
        yield return new WaitForSeconds(0.5f);

        timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    // 方法用于开始Credit页面的隐藏并触发渐变效果
    public void HideCreditPage()
    {
        StartCoroutine(FadeOut());
    }
}
