using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public void GameQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
