using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField] private bool lit = true; //se c'è luce
    [SerializeField] private Light lightPrefab;
    private Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    public bool isLit()
    {
        return lit;
    }

    public void takeLight()
    {
        if (lit)
        {
            lit = false;
            lightPrefab.enabled = false; //disabilita la luce
            material.DisableKeyword("_EMISSION"); //disabilita il bagliore del materiale (non dipende dal prefab luce sull'oggetto)
        }
    }

    public void putLight()
    {
        if(!lit)
        {
            lit = true;
            lightPrefab.enabled = true; //abilita la luce
            material.EnableKeyword("_EMISSION"); //abilita il bagliore del materiale
        }
    }
}
