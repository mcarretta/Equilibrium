﻿using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] public GameObject ui;
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");
    private Animator _animator;

    private FadeAudioSource _fadeAudioSource;

    private void Start()
    {
        _fadeAudioSource = GetComponentInChildren<FadeAudioSource>();

        _animator = ui.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {

        // Disable the enemy renderer to avoid interference with the death animation
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(_fadeAudioSource.StartFade());

            gameObject.GetComponentInChildren<Renderer>().enabled = false;
            StartCoroutine(LoadDeathMenu());
            // TODO stop the player
        }
    }

    private IEnumerator LoadDeathMenu()
    {
        // Enable the death animation
        _animator.SetTrigger(FadeOut);
        yield return new WaitForSeconds(1);
        // After the animation completes show the death menu
        LevelsManager.Instance.LoadDeathMenu();
    }
}
