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

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        emissionColor = material.GetColor("_EmissionColor");
        lfe = GetComponent<LightFlickerEffect>();

        if (!lit) //se luce è spenta
        {
            lfe.enabled = true;
            lightPrefab.SetActive(false);
        }
        else
        {
            lightPrefab.SetActive(true);
            lfe.enabled = false;
        }
    }

    public bool isLit()
    {
        return lit;
    }


    public bool takeLight()
    {
        if (lit)
        {
            //material.DisableKeyword("_EMISSION");
            lfe.enabled = true; //attivo il flickering, se nello script del flickering l'effetto è disattivo la luce sarà leggermente accesa 
            lightPrefab.SetActive(false);
            lit = false;
            return true;
        }
        return false;
    }

    public bool PutLight()
    {
        if (!lit)
        {
            //material.EnableKeyword("_EMISSION");
            lfe.enabled = false;
            material.SetColor("_EmissionColor", emissionColor); //rimetto emissione al massimo
            lightPrefab.SetActive(true);
            lit = true;
            return true;
        }
        return false;
    }
}
