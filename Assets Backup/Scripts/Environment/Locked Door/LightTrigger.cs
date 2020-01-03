using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    private bool triggered = false;

    void Start()
    {
        triggered = false;
    }

    public bool IsTriggered()
    {
        return triggered;
    }

    public void Trigger()
    {
        triggered = !triggered;
        print("bottone = " + triggered);
    }
}
