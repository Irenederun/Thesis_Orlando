using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonDrag : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isDragging = false;

    public Transform fixedPosition;
    public GameObject trueMenu;

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        initialPosition = transform.position;
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (CheckCollision())
        {
            transform.position = fixedPosition.position;
            trueMenu.SetActive(true);
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
