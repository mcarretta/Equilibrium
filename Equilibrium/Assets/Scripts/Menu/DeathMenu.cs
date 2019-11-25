using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    //carica la scena con un indice più piccolo di 1 rispetto a death menù
    //mettere death menù per ultimo
    public void Retry()
    {
        LevelsManager.Instance.LoadCurrentLevel();
    }

    public void Quit()
    {
        Application.Quit();
        print("quitting...");
    }
}
