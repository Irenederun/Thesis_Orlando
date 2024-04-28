using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingPlayerPiecesLoader : MonoBehaviour
{
    [System.Serializable]
    public class TwoSprites
    {
        public bool collaged;
        public bool turnedOff;
        public GameObject topPiece;
        public GameObject bottomPiece;
    }
    public List<TwoSprites> spritesList;
    public GameObject endingLine;
    public SpriteRenderer scarf;
    public SpriteRenderer light;
    public SpriteRenderer spot;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
        endingLine.SetActive(false);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0);
        Init();
    }

    void Init()
    {
        for (int i = 0; i < GameManager.instance.SavedBodyParts.Count; i++)
        {
            spritesList[i].collaged = GameManager.instance.SavedBodyParts[i].used;
            FadeInObjectsOverTime(spritesList[i].topPiece, spritesList[i].bottomPiece, 2f);
            StartCoroutine(FadeIn(scarf, 2f));
            StartCoroutine(FadeInSpotlight(light, 2f));
            StartCoroutine(FadeInSpotlight(spot, 2f));
        }
    }

    public void TurnOffPiece()
    {
        bool haveFound = false;

        // try to look for collaged 
        for (int i = 0; i < spritesList.Count; i++)
        {
            if (spritesList[i].collaged)
            {
                if (!spritesList[i].turnedOff)
                {
                    spritesList[i].topPiece.SetActive(false);
                    spritesList[i].bottomPiece.SetActive(false);
                    spritesList[i].turnedOff = true;
                    haveFound = true;
                    return;
                }
            }
        }
        
        // no collaged; try to look for one that isnt turnedoff
        if (!haveFound)
        {
            // for (int i = 0; i < spritesList.Count; i++)
            // {
            //     if (!spritesList[i].turnedOff)
            //     {
            //         spritesList[i].topPiece.SetActive(false);
            //         spritesList[i].bottomPiece.SetActive(false);
            //         spritesList[i].turnedOff = true;
            //         haveFound = true;
            //         return;
            //     }
            // }
            
            List<int> availableIndices = new List<int>();
            
            for (int i = 0; i < spritesList.Count; i++)
            {
                if (!spritesList[i].turnedOff)
                {
                    availableIndices.Add(i);
                }
            }

            if (availableIndices.Count > 0)
            {
                int randomIndex = Random.Range(0, availableIndices.Count);
                int selectedIndex = availableIndices[randomIndex];
                
                spritesList[selectedIndex].topPiece.SetActive(false);
                spritesList[selectedIndex].bottomPiece.SetActive(false);
                spritesList[selectedIndex].turnedOff = true;
            }
        }
    }

    public void PiecesFadeOut()
    {
        for (int i = 0; i < spritesList.Count; i++)
        {
            if (!spritesList[i].turnedOff)
            {
                GameObject a = spritesList[i].topPiece;
                GameObject b = spritesList[i].bottomPiece;
                spritesList[i].turnedOff = true;
                FadeOutObjectsOverTime(a, b, 4);
            }
        }

        StartCoroutine(FadeOut(light, 4));
        StartCoroutine(FadeOut(spot, 4));
        StartCoroutine(FadeOut(scarf, 4));
    }

    private void FadeOutObjectsOverTime(GameObject a, GameObject b, float fadeDuration)
    {
        // Get the SpriteRenderers of the GameObjects
        SpriteRenderer spriteRendererA = a.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteRendererB = b.GetComponent<SpriteRenderer>();

        // Ensure both SpriteRenderers exist
        if (spriteRendererA != null && spriteRendererB != null)
        {
            // Start fading out each GameObject
            StartCoroutine(FadeOut(spriteRendererA, fadeDuration));
            StartCoroutine(FadeOut(spriteRendererB, fadeDuration));
        }
        else
        {
            Debug.LogError("One or both of the GameObjects do not have a SpriteRenderer component.");
        }
    }

    private IEnumerator FadeOut(SpriteRenderer spriteRenderer, float fadeDuration)
    {
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            spriteRenderer.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        spriteRenderer.color = targetColor;
    }
    
    
    public void TextFadeIn()
    {
        endingLine.SetActive(true);
        StartCoroutine(FadeText(4f));
    }
    
    IEnumerator FadeText(float duration)
    {
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime / duration;

            alpha = Mathf.Clamp01(alpha);

            Color color = endingLine.GetComponent<TextMeshPro>().color;
            color.a = alpha;
            endingLine.GetComponent<TextMeshPro>().color = color;

            yield return null;
        }
    }
    
    
    private void FadeInObjectsOverTime(GameObject a, GameObject b, float fadeDuration)
    {
        SpriteRenderer spriteRendererA = a.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteRendererB = b.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeIn(spriteRendererA, fadeDuration));
        StartCoroutine(FadeIn(spriteRendererB, fadeDuration));
    }

    private IEnumerator FadeIn(SpriteRenderer spriteRenderer, float fadeDuration)
    {
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            spriteRenderer.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        spriteRenderer.color = targetColor;
    }
    
    private IEnumerator FadeInSpotlight(SpriteRenderer spriteRenderer, float fadeDuration)
    {
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 81/255f);

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            spriteRenderer.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        spriteRenderer.color = targetColor;
    }
}
