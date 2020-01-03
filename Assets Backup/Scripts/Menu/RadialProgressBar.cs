using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialProgressBar : MonoBehaviour
{
    [SerializeField] private Transform loadingBar;
    [HideInInspector] public float maxTime = 10f;
    private float ActiveTime = 0f;
    private bool loading = false; //serve quando parte il gioco per avere la barra completamente carica

    public void Update()
    {
        if (!loading)
            return;

        ActiveTime += Time.deltaTime;
        var percent = ActiveTime / maxTime;
        loadingBar.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, percent);
    }

    public void StartLoading()
    {
        ActiveTime = 0;
        loading = true;
    }
}
