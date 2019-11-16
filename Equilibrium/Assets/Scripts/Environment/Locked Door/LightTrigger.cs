using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    private bool triggered;

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
        triggered = true;
    }
}
