using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private enum CameraSate
    {
        Idle,
        Following,
    }
    [SerializeField] private CameraSate camState;

    private Vector3 destinationPos;
    public GameObject actress;
    private float offset;

    public float leftBound;
    public float rightBound;

    // Update is called once per frame
    void Update()
    {
        if (camState == CameraSate.Following)
        {
            if (transform.position.x >= leftBound && transform.position.x < rightBound)
            {
                transform.position =  
                    new Vector3(actress.transform.position.x + offset, transform.position.y, transform.position.z);
            }
            //TODO: this is very problematic the cam stops after reaching right bound b/c cam moving too fast
        }
    }

    public void CameraFollowing(Vector3 actressPos)
    {
        camState = CameraSate.Following;
        offset = transform.position.x - actressPos.x;
        //destinationPos = new Vector3(desPos, transform.position.y, transform.position.z);
        //transform.position = destinationPos;
    }

    public void CameraStopFollowing()
    {
        camState = CameraSate.Idle;
    }
}
