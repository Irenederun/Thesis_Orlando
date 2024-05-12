using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreditBack : MonoBehaviour, IPointerClickHandler
{
    public GameObject creditPage; // Credit page
    public GameObject Spotlight;
    public Image fadeImage; 
    public float fadeDuration = 1.0f; 


    void Start()
    {
        creditPage.SetActive(true); // initially credit page open
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(FadeAndShowMenu());
    }

    IEnumerator FadeAndShowMenu()
    {
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

    }
}