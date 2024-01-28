using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOCManager : MonoBehaviour
{
    public static OOCManager instance;
    public List<GameObject> OOCobjects = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchInteractabilityForAll(bool interactability)
    {
        switch (interactability)
        {
            case true:
                foreach (GameObject obj in OOCobjects)
                {
                    obj.layer = LayerMask.NameToLayer("Interactable");
                }
                break;
            case false:
                foreach (GameObject obj in OOCobjects)
                {
                    obj.layer = LayerMask.NameToLayer("Default");
                }
                break;
        }
    }
}
