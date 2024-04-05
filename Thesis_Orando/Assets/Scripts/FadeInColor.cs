using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeInColor : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public Color targetColor; // 目标颜色
    public float delay = 1f; // 指定延迟开始时间

    private Color startColor = Color.white; // 初始颜色默认为白色
    private bool animationStarted = false;

    void Start()
    {
        // 设置文字的初始颜色为白色
        textMeshPro.color = startColor;

        Invoke("StartAnimation", delay);
        // delay and activate certain function (方法，时间（单位秒）)
    }

    void StartAnimation()
    {
        // A method to start animation
        StartCoroutine(AnimateTextColor()); //启动协程
        animationStarted = true;
    }

    IEnumerator AnimateTextColor()
    {
        float timer = 0f;

        while (timer < 1f)
        {
            timer += Time.deltaTime / delay; // 根据延迟调整计时器
            float t = Mathf.Clamp01(timer); // mathf.clamp01用于限制0到1之间，大于1返回1，小于0返回0
            textMeshPro.color = Color.Lerp(startColor, targetColor, t); // Lerp用于两个之间的线性插值，从起始颜色渐变到目标颜色
            yield return null;
            // yield return null 或 yield return WaitForSeconds(time) 
            //等待一帧结束协程！这样可以让颜色进行渐变而不是在一帧内完成
        }
    }
}
