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
    //private Vector3 velocity = Vector3.zero;
    private float smoothTimeDealing = 0.6f;
    private float arrivalOffset = 0.2f;
    private float smoothDampOffset = 1.5f;
    private Animator anim;
    private SpriteRenderer sp;
    private bool animPlaying = false;
    public float speed;

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
            ActressStopping();
        }
        
        else if (actressState == ActressState.Walking)
        {
            if (Vector3.Distance(transform.position, destinationPos) > smoothDampOffset)
            {
                ActressMovingTowards(speed);
            }
            else if (Vector3.Distance(transform.position, destinationPos) <= arrivalOffset)
            {
                StopActress();
            }
            else if (Vector3.Distance(transform.position, destinationPos) <= smoothDampOffset)
            {
                ActressMovingTowards(speed * 0.7f);
                //ActressSmoothDamp(new Vector3(speed, 0,0));
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

    private void ActressStopping()
    {
        if (!animPlaying)
        {
            anim.SetTrigger("Idle");
            //ICManager.instance.CameraStopFollow();
            animPlaying = true;
            ICManager.instance.DestroyDesPosIcon();
        }
    }

    private void ActressMovingTowards(float curSpeed)
    {
        transform.position = Vector3.MoveTowards(transform.position,destinationPos, curSpeed * Time.deltaTime);
    }

    private void ActressSmoothDamp(Vector3 vel)
    {
        transform.position = Vector3.SmoothDamp
        (transform.position, destinationPos, ref vel, smoothTimeDealing, Mathf.Infinity);
    }
    
    public void StopActress()
    {
        actressState = ActressState.Idle;
    }
}
