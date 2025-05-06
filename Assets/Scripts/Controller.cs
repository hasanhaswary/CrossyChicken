using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadEasyMode()
    {
        SceneManager.LoadScene("EasyMode");
    }

    public void LoadHardMode()
    {
        SceneManager.LoadScene("HardMode");
    }
}
