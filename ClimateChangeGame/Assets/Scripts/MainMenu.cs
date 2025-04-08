using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        //Not added yet since we don't have credits
        Debug.Log("Credits Button Activated");
    }

    public void Controls()
    {
        //Not added yet since we don't have a controls scene yet
        Debug.Log("Control Button Activated");
    }
}
