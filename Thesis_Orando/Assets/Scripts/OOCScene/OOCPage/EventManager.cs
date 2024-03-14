using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    
        
   public UnityEvent m_MyEvent;


   public static EventManager instance;

   private void Awake()
   {
       instance = this;
       if (m_MyEvent == null)
           m_MyEvent = new UnityEvent();
   }

   // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
