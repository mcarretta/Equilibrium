using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHidePanel : MonoBehaviour
{
    public GameObject panel;
    bool state;

    public void ShowHide()
    {
        state = !state;
        panel.gameObject.SetActive(state);
    }
}
