using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBehavior : MonoBehaviour
{   
    public virtual void ClickedByMouse()
    {
        print(gameObject.name + " clicked");
    }
}
