using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //per onTriggerEnter

public class LightBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f; //velocità del proiettile
    [SerializeField] private float lifetime = 5f; //si distrugge dopo n secondi da quando è stato sparato
    private float _timer; //timer per distruggere l'oggetto
    private float _energy;

    private void Start()
    {
        _timer = lifetime;
        _energy = 1f;
    }

    void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime; //muovo il proiettile senza fisica

        _timer -= Time.deltaTime;
        if (_timer <= 0)
            Destroy(gameObject);
    }

    //il proiettile si distrugge se tocca qualcosa
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<AICombat>().hit(_energy);
        }
    }
}
