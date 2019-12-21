using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class AICombat : MonoBehaviour
{
    private float _health;
    private bool _canFade;
    private Color _startColor;
    private MeshRenderer _mMeshRenderer;

    // Death animation variables
    
    public float spawnEffectTime = 2;
    public AnimationCurve fadeIn;

    ParticleSystem ps;
    float m_Timer = 0;
    Renderer m_Renderer;

    int m_ShaderProperty;

    bool m_deathAnimationOn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = 1f;
        _canFade = false;
        _mMeshRenderer = GetComponent<MeshRenderer>();

        m_ShaderProperty = Shader.PropertyToID("_cutoff");
        m_Renderer = GetComponentInChildren<Renderer>();
        ps = GetComponentInChildren <ParticleSystem>();

        var main = ps.main;
        main.duration = spawnEffectTime;
    }

    private void Update()
    {
        if (m_deathAnimationOn)
        {
            m_Timer += Time.deltaTime;
            if (m_Timer > spawnEffectTime)
            {
                m_deathAnimationOn = false;
                AICoordinator.Instance.ProcessDeath(gameObject);
                Destroy(gameObject);
            }
            
            m_Renderer.material.SetFloat(m_ShaderProperty, fadeIn.Evaluate( Mathf.InverseLerp(0, spawnEffectTime, m_Timer)));

        }
    }

    
    public void hit(float energy)
    {
        _health -= energy;
        if (_health <= 0)
        {
            gameObject.GetComponent<AIChase>().AISpeed = 0;
            m_deathAnimationOn = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            ps.Play();
            
        }
            
    }
}
