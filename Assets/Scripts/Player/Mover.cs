using UnityEngine;
using Pathfinding;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [Header("PlayerComponents")]
        [SerializeField] private RichAI agent;

        [HideInInspector] public bool isAttacking;
        //private AttackScript attack;
        private bool initialIddle;
        private bool moving;

        /*  private void Awake()
          {
              attack = GetComponent<AttackScript>();
          }*/

        void FixedUpdate()
        {
            /* if (attack.Combat()) 
                 return;*/
        }


        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        //Set target
        public void MoveTo(Vector3 destination)
        {
            agent.isStopped = false;
            agent.destination = destination;
        }

        //Stop movement
         public void Cancel()
         {
             //animator.SetBool("isWalking", false);
             moving = false;
             agent.isStopped = true;
         }

        //Stop movement without the mouse control
        /*   public void StopV2()
           {
               animator.SetBool("isWalking", false);
               agent.isStopped = true;
           }*/

        /* public void SetMoving(bool state)
         {
             moving = state;
         }*/
    }
}
