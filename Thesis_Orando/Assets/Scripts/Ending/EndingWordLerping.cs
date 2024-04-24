using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingWordLerping : MonoBehaviour
{
    public void MoveWord(Transform target)
    {
        IEnumerator coroutine = MoveTowards(target);
        StartCoroutine(coroutine);
    }

    private IEnumerator MoveTowards(Transform target)
    {
        Vector3 startPos = transform.localPosition;
        float t = 0;
        float duration = 0.6f;
        
        while (gameObject.transform.localPosition != Vector3.zero)
        {
            t += 0.02f / duration;
            float curvedT = cubicHermiteSpline(t);
            transform.localPosition = Vector3.Lerp(startPos, Vector3.zero, curvedT);
            //transform.localPosition = 
            //Vector3.MoveTowards(transform.localPosition,originalPos, 10 * Time.deltaTime);
            yield return new WaitForSeconds(0.02f);
        }
        
    }
    
    private float cubicHermiteSpline(float t)
    {
        return 1 - (2 * t * t * t - 3 * t * t + 1);
    }
}
