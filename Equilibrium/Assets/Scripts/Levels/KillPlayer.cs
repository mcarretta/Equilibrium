using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public GameObject ui;
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            gameObject.GetComponentInChildren<Renderer>().enabled = false;
            StartCoroutine(LoadDeathMenu());
        }
    }

    private IEnumerator LoadDeathMenu()
    {
        //se è un oggetto sul layer player
        Animator animator = ui.GetComponent<Animator>();
        animator.SetTrigger(FadeOut);
        yield return new WaitForSeconds(1);
        LevelsManager.Instance.LoadDeathMenu();
    }
}
