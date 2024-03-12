using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMouse : MonoBehaviour
{
    private float mousepPosX;
    private Vector3 startPos;
    public float moveMinRange;
    public float smoothTime;
    public float moveDistance;
    private Vector3 velocity;
    public ParallexScrolling parallex;
    private float oldx;
    public GameObject actress;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        oldx = startPos.x;
    }

    // Update is called once per frame
    void Update()
    {
        mousepPosX = Input.mousePosition.x;
        //mousepPosX = actress.transform.position.x;
        mousepPosX /= Screen.width;
        mousepPosX = mousepPosX * 2 - 1;//on the scale of -1 to 1

        if (Mathf.Abs(mousepPosX) > moveMinRange)
        {
            Vector3 targetPos = new Vector3((startPos.x + (mousepPosX - moveMinRange * Mathf.Sign(mousepPosX)) * moveDistance), startPos.y, startPos.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        }
        
        float movement = transform.position.x - oldx;
        parallex.UpdatePositions(movement);

        oldx = transform.position.x;
    }
}
