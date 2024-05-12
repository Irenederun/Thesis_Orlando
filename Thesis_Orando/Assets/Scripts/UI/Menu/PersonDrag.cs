using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        initialPosition = transform.position; // move follow mouse
    }

    private void OnMouseUp()
    {
        isDragging = false; 
        personAnimator.SetBool("isDraggingPerson", false);


        if (CheckCollision())
        {
            Color newColor = GetComponent<SpriteRenderer>().color;
            newColor.a = 0f; // define alpha of this obj

            GetComponent<SpriteRenderer>().color = newColor;//set the obj to transparent

            transform.position = fixedPosition.position;
            trueMenu.SetActive(true);
            PersonObject.SetActive(true);
            RopeObject.SetActive(false);
         

            StartCoroutine(DelayRaycastTarget());

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

            if (collider.gameObject.CompareTag("Object2"))
            {
                return true;
            }
        }

        return false;
    }

    IEnumerator DelayRaycastTarget()
    {
        yield return new WaitForSeconds(2.8f);
        Image spotlightImage = spotlightObject.GetComponent<Image>();
        spotlightImage.raycastTarget = false;
        PersonXObject.SetActive(false);

    }
}

/* Log: tried to delay Disable Raycast but PersonXObject would be disabled at the same time
then the script no process,,, so I just put PersonXObject.SetActive(false) in IEnumerator,
set the alpha = 0 when collide
who cares it works :p
 */
