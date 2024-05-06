using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLoadScene : MonoBehaviour
{
    public Image fadeInImage; 
    public float fadeInDuration = 1.0f; 

    public void StartGame()
    {
        StartCoroutine(StartGameWithFade());
    }

    IEnumerator StartGameWithFade()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeInDuration);
            fadeInImage.color = new Color(0, 0, 0, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeInImage.color = new Color(0, 0, 0, 1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}