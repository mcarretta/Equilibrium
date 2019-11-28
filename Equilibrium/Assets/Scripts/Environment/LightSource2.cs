using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource2 : MonoBehaviour
{
    [SerializeField] private bool lit = true;
    [SerializeField] private GameObject lightPrefab; //luce figlia dell'oggetto sorgente di luce
    private Material material; //materiale emissivo

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    public bool isLit()
    {
        return lit;
    }


    public bool takeLight()
    {
        if (lit)
        {
            print("disabilito emissione");
            material.DisableKeyword("_EMISSION");
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
            print("abilito emissione");
            material.EnableKeyword("_EMISSION");
            lightPrefab.SetActive(true);
            lit = true;
            return true;
        }
        return false;
    }
}
