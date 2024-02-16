using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActressController : MonoBehaviour
{
    [SerializeField] private enum ActressState
    {
        Idle,
        Walking,
    }
    [SerializeField] private ActressState actressState;
    
    private Vector3 destinationPos;
    private Vector3 velocity = Vector3.zero;
    private float smoothTimeDealing = 1.2f;
    private float arrivalOffset = 0.4f;
    private Animator anim;
    private SpriteRenderer sp;
    private bool animPlaying = false;

    void Start()
    {
        Mouse.instance.actressController = this;
        actressState = ActressState.Idle;
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (actressState == ActressState.Idle)
        {
            if (!animPlaying)
            {
                anim.SetTrigger("Idle");
                ICManager.instance.CameraStopFollow();
                animPlaying = true;
            }
        }
        
        else if (actressState == ActressState.Walking)
        {
            transform.position = Vector3.SmoothDamp
                (transform.position, destinationPos, ref velocity, smoothTimeDealing, Mathf.Infinity);
            
            if (Vector3.Distance(transform.position, destinationPos) <= arrivalOffset)
            {
                actressState = ActressState.Idle;
            }
        }
    }

    public void LerpToPos(float desPos)
    {
        ICManager.instance.CameraFollow();
        actressState = ActressState.Walking;
        anim.SetTrigger("Walking");
        animPlaying = false;
        if (desPos < transform.position.x)
        {
            sp.flipX = true;
        }
        else
        {
            sp.flipX = false;
        }
        destinationPos = new Vector3(desPos, transform.position.y, 0f);
    }
}
