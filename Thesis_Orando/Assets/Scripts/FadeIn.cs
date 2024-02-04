using System.Collections;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SelfDes());
    }

    IEnumerator SelfDes()
    {
        yield return new WaitForSeconds(1.1f);
        Destroy(gameObject);
    }
}
