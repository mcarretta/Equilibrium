using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        LevelsManager.Instance.LoadFirstLevel();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
