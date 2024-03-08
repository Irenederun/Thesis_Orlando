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
    private float arrivalOffset = 0.002f;
    private float smoothDampOffset = 1.5f;
    private Animator anim;
    private SpriteRenderer sp;
    private bool animPlaying = false;
    public float speed;
    public bool isLeft;

    [SerializeField]
    private float leftBound;
    [SerializeField]
    private float rightBound;

    private bool walkable = true;
    private Transform destIcon;

    void Start()
    {
        MouseRayCast.instance.actressController = this;
        actressState = ActressState.Idle;
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        if (ExchangeGameManager.instance != null) ExchangeGameManager.instance.oocMainCharLimbSync = gameObject.GetComponent<LimbSync>();
    }

    void Update()
    {
        //issue: if player leave unity when actress is walking, she cannot walk anymore after returning to unity
        //i think it is something about progress being force stopped and state changes messed up

        if (!walkable)
        {
            return;
        }
        
        if (actressState == ActressState.Idle)
        {
            ActressStopping();
        }
        
        else if (actressState == ActressState.Walking)
        {
            float desPos = destIcon.position.x;
            if (desPos < transform.position.x)
            {
                sp.flipX = true;
                isLeft = true;

            }
            else
            {
                sp.flipX = false;
                isLeft = false;
            }
        
            destinationPos = new Vector3(desPos, transform.position.y, 0f);
            
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

    public void StartWalking(Transform _destIcon)
    {
        ICManager.instance.CameraFollow();
        actressState = ActressState.Walking;
        anim.SetTrigger("Walking");
        animPlaying = false;

        destIcon = _destIcon;
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
        //ICManager.instance.ChangeParallex(false,false);
    }

    public void DisableWalking()
    {
        walkable = false;
    }

    public void EnableWalking()
    {
        walkable = true;
    }
}
