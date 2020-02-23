using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        LevelsManager.Instance.LoadFirstLevel();
    }

    public void LoadCredits()
    {
        LevelsManager.Instance.LoadLevelByIndex(5);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        LevelsManager.Instance.LoadMainMenu();
    }
}
