using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCreator : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;


    void Start()//Creates a button and sets it up
    {
        List<string> levels = LevelsManager.Instance.getLevels();
        int index = 0;
        foreach (string lv in levels)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(panelToAttachButtonsTo.transform);//Setting button parent
            button.GetComponent<Button>().onClick.AddListener(OnClick);
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = lv;
            button.name = index.ToString();
            index++;
        }
    }

    void OnClick()
    {
        int i = Int32.Parse(EventSystem.current.currentSelectedGameObject.name);
        LevelsManager.Instance.LoadLevelByIndex(i);
    }
}