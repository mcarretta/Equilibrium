using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField] private bool lit = true; //se c'è luce
    [SerializeField] private Light lightPrefab;

    public bool isLit()
    {
        return lit;
    }

    public void takeLight()
    {
        if (lit)
        {
            lit = false;
            lightPrefab.enabled = false;
        }
    }

    public void putLight()
    {
        if(!lit)
        {
            lit = true;
            lightPrefab.enabled = true;
        }
    }
}
