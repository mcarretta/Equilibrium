using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;


    void Update()
    {
        if(Input.GetButtonDown("PauseMenu"))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); //abilito il menù di pausa
        Time.timeScale = 1f; //tempo normale di scorrimento del gioco
        gameIsPaused = false;
        if(!UnityStandardAssets.Characters.FirstPerson.MouseLook.joystickConnected)
        {
            Cursor.lockState = CursorLockMode.Locked; //blocco il cursore al centro
            Cursor.visible = false; //lo rendo invisibile
        }
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true); //abilito il menù di pausa
        Time.timeScale = 0f; //metto in pausa il gioco
        gameIsPaused = true;
        if (!UnityStandardAssets.Characters.FirstPerson.MouseLook.joystickConnected)
        {
            Cursor.lockState = CursorLockMode.None; //sblocco il cursore per usare il menu
            Cursor.visible = true; //lo rendo visibile
        }
    } 

    public void Retry()
    {
        LevelsManager.Instance.LoadCurrentLevel();
        Resume();
    }

    public void Quit()
    {
        gameIsPaused = false;
        Time.timeScale = 1f; //tempo normale di scorrimento del gioco
        if (!UnityStandardAssets.Characters.FirstPerson.MouseLook.joystickConnected)
        {
            Cursor.lockState = CursorLockMode.None; //sblocco il cursore per usare il menu
            Cursor.visible = true; //lo rendo visibile
        }
        LevelsManager.Instance.LoadMainMenu();
    }
}
