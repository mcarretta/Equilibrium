using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousLevelWarp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        LevelsManager.Instance.LoadPreviousLevel();
    }
}
