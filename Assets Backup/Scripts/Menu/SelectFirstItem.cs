using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectFirstItem : MonoBehaviour
{
    public Button selectedButton; //il bottone che deve essere già selezionato all'apertura del menu
    private EventSystem eventSystem;

    private void OnEnable()
    {
        //se non è connesso il joystick non serve evidenziare il primo elemento
        if (!UnityStandardAssets.Characters.FirstPerson.MouseLook.joystickConnected)
            return;
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        StartCoroutine(SelectBtnFix());
        print("seleziono bottone");
    }

    //per fixare un bug di unity, coroutine che evidenzia il bottone in due passaggi
    private IEnumerator SelectBtnFix()
    {
        eventSystem.SetSelectedGameObject(null);
        yield return null;
        eventSystem.SetSelectedGameObject(selectedButton.gameObject);
    }
}
