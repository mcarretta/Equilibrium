using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class AIChase : MonoBehaviour
    {
        // Start is called before the first frame update

        [SerializeField] private LayerMask layerMask;
        
        public float stopDistance = 50;
        public float sightDistance = 100f;
        public float AISpeed =0.4f;
        public bool isSearching;
        public bool enableRayCastingVision = true;
        public float proximityThreshold = 5;
        public GameObject player;
        
        private GameObject _aiGameObject;
        private NavMeshAgent _mNavMeshAgent;
        private string _playerColliderName;
       

        void Start()
        {
            layerMask = ~layerMask;
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
                        _mNavMeshAgent.speed = AISpeed;
                    }
                    // Stop only if player is far enough
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
            var transform1 = transform;
            Vector3 aiForward = transform1.forward;
            Vector3 playerDirection = (playerGameObject.transform.position - transform1.position).normalized;
        
            //Debug.DrawRay(transform.position, aiForward, Color.blue);
            //Debug.DrawLine(transform.position, playerGameObject.transform.position, Color.red);

            // If the player is in the FOV of the AI, then ray cast to check if he is hidden behind objects
            if (Vector3.Angle(aiForward, playerDirection) < 62f)
            {
                // Debug.Log("In sight!");
                
                Ray forwardRay = new Ray(transform.TransformPoint(0,0,0.7f), playerDirection);
                RaycastHit hit;
                
                if (Physics.Raycast(forwardRay, out hit, sightDistance) && hit.collider.name == playerCollider)
                {
                    return true;
                }
            }
            
            else if (Vector3.Distance(player.transform.position, transform.position) < proximityThreshold)
            {
                
                Ray forwardRay = new Ray(transform.TransformPoint(0,0,0), playerDirection);
                RaycastHit hit;
                
                //Debug.Log("Can be sensed");
                if (Physics.Raycast(forwardRay, out hit, proximityThreshold, layerMask))
                {
                    if(hit.collider.name == playerCollider)
                    {
                        //Debug.Log("Spider-Sense!");
                        return true;
                    }
                } 
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
