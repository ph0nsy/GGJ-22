using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Scene_Manager : MonoBehaviour
{
    public GameObject canvas_Settings;
    public GameObject canvas_Menu;
    public void openGitHub()
    {
        Application.OpenURL("https://github.com/ph0nsy/GGJ-22");
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void openSettings()
    {
        canvas_Menu.SetActive(false);
        canvas_Settings.SetActive(true);
    }
    public void closeSettings()
    {
        canvas_Menu.SetActive(true);
        canvas_Settings.SetActive(false);
    }
}
