using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICombat : MonoBehaviour
{
    private float _health;
    private bool _canFade;
    private Color _startColor;
    private MeshRenderer _mMeshRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = 1f;
        _canFade = false;
        _mMeshRenderer = GetComponent<MeshRenderer>();
        //_startColor = Color.red;
        //_endColor = 
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            hit(25f);
        }
    }

    
    public void hit(float energy)
    {
        _health -= energy;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
            
    }
}
