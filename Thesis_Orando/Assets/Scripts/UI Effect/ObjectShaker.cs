using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShaker : MonoBehaviour
{
    public float shakeSpeed = 1.0f; // speed
    public float shakeRange = 0.1f; // range

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        // initial pos/rotation
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        // calculate shake amount
        float shakeAmountX = Mathf.Sin(Time.time * shakeSpeed) * shakeRange;
        float shakeAmountY = Mathf.Cos(Time.time * shakeSpeed) * shakeRange;

        // apply
        Vector3 newPosition = startPosition + new Vector3(shakeAmountX, shakeAmountY, 0);
        Quaternion newRotation = startRotation * Quaternion.Euler(new Vector3(shakeAmountX, shakeAmountY, 0));

        // new pos / rota
        transform.position = newPosition;
        transform.rotation = newRotation;
    }
}
