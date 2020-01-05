using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] public GameObject ui;
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator = ui.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Disable the enemy renderer to avoid interference with the death animation
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            gameObject.GetComponentInChildren<Renderer>().enabled = false;
            StartCoroutine(LoadDeathMenu());
            // TODO stop the player
        }
    }

    private IEnumerator LoadDeathMenu()
    {
        // Enable the death animation
        animator.SetTrigger(FadeOut);
        yield return new WaitForSeconds(1);
        // After the animation completes show the death menu
        LevelsManager.Instance.LoadDeathMenu();
    }
}
