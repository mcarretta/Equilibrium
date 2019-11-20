using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class AIChase : MonoBehaviour
    {
        // Start is called before the first frame update
        
        public float stopDistance = 50;
        public bool isSearching;
        public bool enableRayCastingVision = true;
        public GameObject player;
        
        private GameObject _aiGameObject;
        private NavMeshAgent _mNavMeshAgent;
        private string _playerColliderName;
       

        void Start()
        {
            _aiGameObject = gameObject;
            _mNavMeshAgent = _aiGameObject.GetComponent<NavMeshAgent>();
            _playerColliderName = player.GetComponent<Collider>().name;

        }

        // Update is called once per frame
        void Update()
        {
        
            // Proceed towards the player zombie-like if triggered by events
            if (isSearching)
            {
                // TODO place here the proportionality to distance
                _mNavMeshAgent.speed = 0.2f;
            }
            else
            {
                // If the player is in sight proceed faster, otherwise stay still
                if (enableRayCastingVision)
                {
                    if (IsInSight(player, _playerColliderName))
                    {
                        _mNavMeshAgent.speed = 0.4f;
                    }
                    // Stop only if player is far enough
                    // TODO define stopping distance
                    else if (Vector3.Distance(player.transform.position, transform.position) > stopDistance)
                    {
                        _mNavMeshAgent.speed = 0;
                    }
                
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

        public void TriggerChaseIfInDistance(float thresholdDistance)
        {
            // React to some player's action by chasing him if he is in range
            
            if (Vector3.Distance(player.transform.position, transform.position) < thresholdDistance) 
                isSearching = true;
        }
    }
}
