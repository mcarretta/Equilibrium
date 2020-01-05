using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource2 : MonoBehaviour
{
    [SerializeField] private bool lit = true;
    [SerializeField] private GameObject lightPrefab; //luce figlia dell'oggetto sorgente di luce
    private Material material; //materiale emissivo
    private Color emissionColor;
    private LightFlickerEffect lfe;
    private float maxIntensity = 0;

    private void Start()
    {
        //init
        material = GetComponent<Renderer>().material;
        emissionColor = material.GetColor("_EmissionColor");
        lfe = GetComponent<LightFlickerEffect>();
        maxIntensity = lightPrefab.GetComponentInChildren<Light>().intensity;

        if (!lit) //se luce è spenta
            TurnOffLight();
        else
            TurnOnLight();
    }

    public bool isLit()
    {
        return lit;
    }


    public bool takeLight()
    {
        if (lit)
        {
            TurnOffLight();
            return true;
        }
        return false;
    }

    public bool PutLight()
    {
        if (!lit)
        {
            TurnOnLight();
            return true;
        }
        return false;
    }

    private void TurnOffLight()
    {
        foreach (Light l in lightPrefab.GetComponentsInChildren<Light>())
            l.intensity = 0;
        lfe.enabled = true;
        lit = false;
    }

    private void TurnOnLight()
    {
        foreach (Light l in lightPrefab.GetComponentsInChildren<Light>())
            l.intensity = maxIntensity;
        material.SetColor("_EmissionColor", emissionColor); //rimetto emissione al massimo
        lfe.enabled = false;
        lit = true;
    }
}
