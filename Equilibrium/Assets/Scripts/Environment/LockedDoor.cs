using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] List<LightTrigger> triggerList = new List<LightTrigger>();
    private int triggerCount = 0;
    private int triggerActive = 0;

    void Start()
    {
        triggerCount = triggerList.Count;
    }

}
