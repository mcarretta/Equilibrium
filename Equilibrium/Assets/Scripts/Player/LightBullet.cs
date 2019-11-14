using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //per onTriggerEnter

public class LightBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f; //velocità del proiettile
    [SerializeField] private float lifetime = 5f; //si distrugge dopo n secondi da quando è stato sparato
    private float timer; //timer per distruggere l'oggetto

    private void Start()
    {
        timer = lifetime;    
    }

    void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime; //muovo il proiettile senza fisica

        timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);
    }

    //il proiettile si distrugge se tocca qualcosa
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
