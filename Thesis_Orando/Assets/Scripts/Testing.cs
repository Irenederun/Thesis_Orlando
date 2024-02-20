using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Vector3 mousePos;
    RaycastHit hitData;

    // Update is called once per frame
    void Update()
    {
        // mousePos = Input.mousePosition;
        // mousePos.z = Camera.main.nearClipPlane;
        // Vector3 worldPos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        
        var screenPoint = Input.mousePosition;
        screenPoint.z = 9.0f; //distance of the plane from the camera
        mousePos = Camera.main.ScreenToWorldPoint(screenPoint);
        
        // RaycastHit hit =
        //     Physics.Raycast(mousePos, Vector3.back, out RaycastHit,
        //         Mathf.Infinity, LayerMask.GetMask("ClickableAreaMoving"));

        //Ray ray = new Ray(mousePos, Vector3.forward);
        //RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            //bool returnHit = Physics.Raycast
                //(mousePos, transform.TransformDirection(Vector3.forward),
                //out hit, Mathf.Infinity, LayerMask.GetMask("ClickableAreaMoving"));
            
            
            //print(returnHit + " " + mousePos + " " + hit.point);
            RaycastHit2D hit2 = Physics2D.GetRayIntersection(new Ray(mousePos, Vector3.forward), Mathf.Infinity, LayerMask.GetMask("ClickableAreaMoving"));
         //   RaycastHit2D.GetRa
          //  RaycastHit2D.GetRayIntersection()
            print(hit2.collider.gameObject.name  + " " + hit2.point);   
           // RaycastHit2D.GetRayIntersection(new Ray(mousePos, Vector3.forward), Mathf.Infinity, LayerMask.GetMask("ClickableAreaMoving"));
        }
        Debug.DrawRay(mousePos, Vector3.forward * 10, Color.blue);
        
    }
}
