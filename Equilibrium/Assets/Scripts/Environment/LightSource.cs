using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField] private int intensity = 2; //intensità della luce attuale
    [SerializeField] private int maxIntensity = 2; //massima intensità raggiungibile
    [SerializeField] private Light lightPrefab; //luce figlia dell'oggetto sorgente di luce
    private Material material; //materiale emissivo

    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    //costanti usate per calcolare l'emissione del materiale
    private const float off = 0f;
    private const float dim = 0.3f;
    private const float bright = 1f;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        lightPrefab.intensity = intensity;
        calcEmission();
    }

    public int getIntensity()
    {
        return intensity;
    }

    public int getMaxIntensity()
    {
        return maxIntensity;
    }

    public void takeLight()
    {
        if (intensity > 0)
        {
            intensity--;
            lightPrefab.intensity = intensity;
            calcEmission();
        }

        if (intensity == 0)
        {
            lightPrefab.enabled = false; //se non c'è luce disabilito il prefab
            AICoordinator.Instance.ProcessEventTrigger(100.0f);
        }
            
    }

    public void putLight()
    {
        if (intensity < maxIntensity)
        {
            intensity++;
            lightPrefab.intensity = intensity;
            lightPrefab.enabled = true; //la luce poteva essere spenta
            calcEmission();
        }
    }

    //imposta l'emissione del materiale in base al valore dell'intensità della luce
    private void calcEmission()
    {
        Color baseColor = Color.white;
        float emission = 0;
        if (intensity == 0)
            emission = off;
        else if (intensity == 1)
            emission = dim;
        else
            emission = bright;
        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
        material.SetColor(EmissionColor, finalColor);
    }
}
