using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;

public class DarknessAI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public GameObject player;
    [SerializeField] private GameObject aiGameObject;
    [SerializeField] private NavMeshAgent mNavMeshAgent;
    private string _playerColliderName;
    public bool isSearching;
    public bool enableRayCastingVision;

    void Start()
    {
        aiGameObject = gameObject;
        mNavMeshAgent = aiGameObject.GetComponent<NavMeshAgent>();
        _playerColliderName = player.GetComponent<Collider>().name;
        isSearching = false;

    }

    // Update is called once per frame
    void Update()
    {
        
        // Proceed towards the player zombie-like if triggered by events
        if (isSearching)
        {
            // TODO place here the proportionality to distance
            mNavMeshAgent.speed = 0.2f;
        }
        else
        {
            // If the player is in sight proceed faster, otherwise stay still
            if (enableRayCastingVision)
            {
                if (IsInSight(player, _playerColliderName))
                {
                    mNavMeshAgent.speed = 0.4f;
                }
                else mNavMeshAgent.speed = 0;
            }
        }
    }

    bool IsInSight(GameObject playerGameObject, string playerCollider)
    {
        // Compute if the player can currently be seen from an AI
        
        // Consider FOV first
        Vector3 aiForward = transform.forward;
        Vector3 playerDirection = (playerGameObject.transform.position - transform.position).normalized;
        
        // Debug.DrawRay(transform.position, aiForward, Color.blue);
        // Debug.DrawLine(transform.position, playerGameObject.transform.position, Color.red);
        
        if (Vector3.Angle(aiForward, playerDirection) < 62f)
        {
            // Debug.Log("In sight!");
            
            // If the player is in the FOV of the AI, then ray cast to check if he is hidden behind objects
            Ray forwardRay = new Ray(transform.position, playerDirection);
            RaycastHit hit;
            
            // Then, check if the player is hidden by objects
            if (Physics.Raycast(forwardRay, out hit, 100f) && hit.collider.name == playerCollider)
            {
                return true;
            }

            return false;
        }
        
        return false;
    }
}
