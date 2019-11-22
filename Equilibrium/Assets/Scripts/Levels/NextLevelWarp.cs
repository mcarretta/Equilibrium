using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelWarp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        LevelsManager.Instance.LoadNextLevel();
    }
}
