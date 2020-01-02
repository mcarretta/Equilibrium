using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI
{
    public class AICoordinator : Singleton<AICoordinator>
    {
        private List<GameObject> _enemies;

        protected override void Awake()
        {
            //Debug.Log("Started");
            _enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        }

        public void ProcessEventTrigger(float triggerDistance)
        {
            Debug.Log("Triggered");
            foreach (GameObject enemy in _enemies)
                enemy.GetComponent<AIChase>().TriggerChaseIfInDistance(triggerDistance);
        }

        public void ProcessDeath(GameObject enemy)
        {
            _enemies.Remove(enemy);
            //Debug.Log("Removed!");
        }
    }
    
}

