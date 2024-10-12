using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUpdate : MonoBehaviour
{
    public Transform cursor;
    public Vector3 clickOffset;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Cursor.visible)
        {
            Cursor.visible = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.position = Input.mousePosition + clickOffset;

        if (Cursor.visible)
        {
            Cursor.visible = false;
        }
    }
}
