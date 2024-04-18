using UnityEngine;
using TMPro;

public class TextGlowing : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public float glowFrequency = 1.0f; // 控制闪烁频率
    public float glowAmplitude = 0.2f; // 控制闪烁强度
    public float outlineSoftness = 1.0f; // 控制软化程度
    public float outlineWidth = 0.2f; // 控制膨胀程度

    private Material textMaterial;
    private float timeOffset;

    void Start()
    {
        if (textMeshPro == null)
            textMeshPro = GetComponent<TextMeshPro>();

        textMaterial = textMeshPro.GetComponent<Renderer>().material;
        timeOffset = Random.Range(0f, 2f); // 随机时间偏移，以避免所有文本同时闪烁
    }

    void Update()
    {
        // 计算闪烁值
        float glowValue = Mathf.Sin((Time.time + timeOffset) * glowFrequency) * glowAmplitude + 1.0f;

        // 应用到外边缘宽度
        textMaterial.SetFloat("_OutlineWidth", outlineWidth * glowValue);

        // 应用到软化程度
        textMaterial.SetFloat("_OutlineSoftness", outlineSoftness);
    }
}
