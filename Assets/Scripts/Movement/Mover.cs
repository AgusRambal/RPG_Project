using UnityEngine;
using Pathfinding;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [Header("PlayerComponents")]
        [SerializeField] private RichAI agent;

        //Flags
        [HideInInspector] public bool moving;
        Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            agent.enabled = !health.IsDead();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        //Set target
        public void MoveTo(Vector3 destination)
        {
            moving = true;
            agent.isStopped = false;
            agent.destination = destination;
        }

        //Stop movement
         public void Cancel()
         {
             moving = false;
             agent.isStopped = true;
         }
    }
}
