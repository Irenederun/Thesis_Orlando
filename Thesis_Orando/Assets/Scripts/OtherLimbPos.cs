using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherLimbPos : MonoBehaviour
{
    public Transform despos;

    // Start is called before the first frame update
    void Update()
    {
        transform.position= despos.position;
    }
}
