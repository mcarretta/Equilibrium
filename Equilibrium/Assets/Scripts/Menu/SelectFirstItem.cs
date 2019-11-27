using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectFirstItem : MonoBehaviour
{
    public Button selectedButton; //il bottone che deve essere già selezionato all'apertura del menu
    public EventSystem eventSystem;

    private void Start()
    {
        Select();
    }

    private void OnEnable()
    {
        Select();
    }

    private void Select()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(selectedButton.gameObject);
    }
}
