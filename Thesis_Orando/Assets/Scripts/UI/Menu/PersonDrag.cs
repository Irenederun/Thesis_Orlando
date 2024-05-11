using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonDrag : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isDragging = false;
    private Animator spotlightAnimator;

    public Transform fixedPosition;
    public Animator personAnimator;
    public GameObject trueMenu;
    public GameObject spotlightObject;
    public GameObject PersonObject;
    public GameObject RopeObject;
    public GameObject PersonXObject;

    private void Start()
    {
        spotlightAnimator = spotlightObject.GetComponent<Animator>();

    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            personAnimator.SetBool("isDraggingPerson", true);
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        AudioManager.instance.PlayAudio("PickUp", GetComponent<AudioSource>());
        initialPosition = transform.position;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        personAnimator.SetBool("isDraggingPerson", false);

        if (CheckCollision())
        {
            transform.position = fixedPosition.position;
            trueMenu.SetActive(true);
            PersonObject.SetActive(true);
            RopeObject.SetActive(false);
            PersonXObject.SetActive(false);
            if (spotlightAnimator != null)
            {
                spotlightAnimator.SetBool("Disappear", true);
            }
        }
        else
        {
            transform.position = initialPosition;
        }
    }

    private bool CheckCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;

            if(collider.gameObject.CompareTag("Object2"))
            {
                return true;
            }
        }

        return false;
    }
}
