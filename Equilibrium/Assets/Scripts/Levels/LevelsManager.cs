using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : Singleton<LevelsManager>
{
    [SerializeField] private List<string> levels;
    [SerializeField] private string deathMenu;
    private int index = 0;

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(levels[index]);
    }

    public void LoadNextLevel()
    {
        if (index >= levels.Count)
        {
            print("non c'è un livello successivo");
            return;
        }
        ++index;
        SceneManager.LoadScene(levels[index]);
    }

    public void LoadPreviousLevel()
    {
        if (index <= 0)
        {
            print("non c'è un livello precedente");
            return;
        }

        --index;
        SceneManager.LoadScene(levels[index]);
    }

    public void LoadDeathMenu()
    {
        SceneManager.LoadScene(deathMenu);
    }


}
