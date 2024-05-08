using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainmenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Solve To Survive");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
   
}
