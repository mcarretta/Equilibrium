using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtField;
    [SerializeField] private GameObject panel;
    [SerializeField] private string text;
    [SerializeField] private bool triggered;

    void Start()
    {
        panel.SetActive(false);
        triggered = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            panel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;
        print("triggered");
        panel.SetActive(true);
        txtField.GetComponent<TextMeshProUGUI>().text = text.Replace("\\n", "\n");
    }
}
