using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : Singleton<LevelsManager>
{
    [SerializeField] private List<string> levels;
    [SerializeField] private string deathMenu;
    [SerializeField] private string mainMenu;

    private int index = 0;

    public List<string> getLevels()
    {
        return new List<string>(levels);
    }

    //carica un livello dato l'indice nella lista
    public void LoadLevelByIndex(int i)
    {
        if (index + 1 >= levels.Count || index < 0)
            print("indice inesistente");
        else
        {
            index = i;
            SceneManager.LoadScene(levels[index]);
        }
    }

    //carica il livello attivo dopo la morte o con retry
    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(levels[index]);
    }

    //carica il primo livello del gioco con new game
    public void LoadFirstLevel()
    {
        print("ready to load the first level");
        index = 0;
        SceneManager.LoadScene(levels[index]);
    }

    //carica il livello successivo quando si arriva alla fine di un livello
    public void LoadNextLevel()
    {
        print("ready to load next level... index == " + index);
        if (index + 1 >= levels.Count)
        {
            print("non c'è un livello successivo");
            return;
        }
        ++index;
        SceneManager.LoadScene(levels[index]);
    }

    //carica livello precedente
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

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    //menu quando si muore
    public void LoadDeathMenu()
    {
        SceneManager.LoadScene(deathMenu);
    }


}
