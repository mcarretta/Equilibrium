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
    private bool _isAnimatingExplosion;
    public float _explosionEffectTime = 2.0f;
    public ParticleSystem Explosion;
    float m_Timer = 0;
    Renderer m_Renderer;

    private Light _bulletLight;
    private MeshRenderer _bulletMesh;
    private SphereCollider _bulletCollider;

    private void Start()
    {
        _timer = lifetime;
        _energy = 1f;

        _bulletLight = GetComponentInChildren<Light>();
        _bulletMesh = GetComponentInChildren<MeshRenderer>();
        _bulletCollider = GetComponentInChildren<SphereCollider>();
       

        var main = Explosion.main;
        main.duration = _explosionEffectTime;
    }

    void Update()
    {
        if (_isAnimatingExplosion)
        {
            _timer += Time.deltaTime;
            if (_timer > _explosionEffectTime)
            {
                _isAnimatingExplosion = false;
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position =
                transform.position + transform.forward * speed * Time.deltaTime; //muovo il proiettile senza fisica

            _timer -= Time.deltaTime;
            if (_timer <= 0)
                Destroy(gameObject);
        }
    }

    //il proiettile si distrugge se tocca qualcosa
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Tutorial"))
        {
            _isAnimatingExplosion = true;
            _timer = 0;
            _bulletLight.enabled = false;
            _bulletCollider.enabled = false;
            _bulletMesh.enabled = false;
        
            Explosion.Play();
        
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<AICombat>().hit(_energy);
            }
        }
        
    }
}
