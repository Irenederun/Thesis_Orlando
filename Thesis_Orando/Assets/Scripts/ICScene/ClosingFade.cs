using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingFade : MonoBehaviour
{
    public List<SpriteRenderer> group1;
    public List<SpriteRenderer> group2;
    public List<SpriteRenderer> group3;
    public List<SpriteRenderer> group4;
    public float fadeDuration;


    private void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        FadeOutCaller();
    }

    public void FadeOutCaller()
    {
        StartCoroutine(FadeOutByGroup());
    }

    IEnumerator FadeOutByGroup()
    {
        StartCoroutine(FadeOutGroups(group1));
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeOutGroups(group2));
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeOutGroups(group3));
        yield return new WaitForSeconds(1 + fadeDuration);
        //StartCoroutine(FadeIn(group4));
        DialogueManager.instance.TriggerDialogue("End");
    }

    IEnumerator FadeOutGroups(List<SpriteRenderer> group)
    {
        foreach (SpriteRenderer sp in group)
        {
            StartCoroutine(IndividualFade(sp));
            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator IndividualFade(SpriteRenderer sp)
    {
        float elapsedTime = 0f;
        Color startColor = sp.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            sp.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        sp.color = targetColor;
    }

    IEnumerator FadeIn(List<SpriteRenderer> group)
    {
        foreach (SpriteRenderer sp in group)
        {
            StartCoroutine(IndividualFadeIn(sp));
            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator IndividualFadeIn(SpriteRenderer sp)
    {
        float elapsedTime = 0f;
        Color startColor = sp.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            sp.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        sp.color = targetColor;
    }
}
