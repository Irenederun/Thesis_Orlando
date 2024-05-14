using System;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
    public static RestartManager instance;
    public Action restartAction;

    public int station = 1;
    
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            station = 1;
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            station = 2;
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            station = 3;
        }
    }

    public void RestartGame()
    {
        //GameObject.Find("FungusManager").transform.GetChild(0).GetComponent<Flowchart>().Reset(false, true);
        FungusManager.Instance.GlobalVariables.variables.Clear();
        GameObject.Find("FungusManager").transform.GetChild(0).GetComponent<Flowchart>().Variables.Clear();
        restartAction?.Invoke();
        SceneManager.LoadScene(0);
    }
}
