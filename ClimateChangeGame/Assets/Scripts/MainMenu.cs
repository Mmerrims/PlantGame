using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject ControlScreen;
    [SerializeField] private GameObject Buttons;

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
        //Hi Cam! You don't need to modify anything in this script to get the controls screen working
        //Just change the GameObjects in the scene. :3
        Debug.Log("Control Button Activated");
        if (Buttons.activeSelf == true) 
        {
            ControlScreen.SetActive(true);
            Buttons.SetActive(false);
        }
        else if (Buttons.activeSelf == false) 
        {
            ControlScreen.SetActive(false);
            Buttons.SetActive(true);
            
        }

    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
