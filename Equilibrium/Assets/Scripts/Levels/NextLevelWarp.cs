using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelWarp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) //se è un oggetto sul layer player
            LevelsManager.Instance.LoadNextLevel();
    }
}
