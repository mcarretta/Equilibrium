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
            print("LoadLevelByIndex: indice inesistente");
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
        print("LoadCurrentLevel: success");
    }

    //carica il primo livello del gioco con new game
    public void LoadFirstLevel()
    {
        index = 0;
        SceneManager.LoadScene(levels[index]);
        print("LoadFirstLevel: success");
    }

    //carica il livello successivo quando si arriva alla fine di un livello
    public void LoadNextLevel()
    {
        if (index + 1 >= levels.Count)
        {
            print("LoadNextLevel: level not found");
            return;
        }
        ++index;
        SceneManager.LoadScene(levels[index]);
        print("LoadNextLevel: success on level of index " + index);
    }

    //carica livello precedente
    public void LoadPreviousLevel()
    {
        if (index <= 0)
        {
            print("LoadPreviousLevel: level not found");
            return;
        }

        --index;
        print("LoadPreviousLevel: success on level of index " + index);
        SceneManager.LoadScene(levels[index]);
    }

    public void LoadMainMenu()
    {
        print("LoadMainMenu: success");
        SceneManager.LoadScene(mainMenu);
    }

    //menu quando si muore
    public void LoadDeathMenu()
    {
        print("LoadDeathMenu: success");
        SceneManager.LoadScene(deathMenu);
    }


}
