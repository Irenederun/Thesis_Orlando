using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Sprite hoverSprite;
    public Sprite clickSprite;
    public GameObject creditPage; 
    public Image fadeImage; 
    public float fadeDuration = 1.0f; 

    private Image buttonImage;
    private Sprite defaultSprite;
    private bool isFading = false;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        defaultSprite = buttonImage.sprite;
        creditPage.SetActive(false); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isFading)
        {
            buttonImage.sprite = hoverSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isFading)
        {
            buttonImage.sprite = defaultSprite; 
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

        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        creditPage.SetActive(true);

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