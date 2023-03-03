using UnityEngine;
using Pathfinding;
using RPG.Core;
using RPG.Saving;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [Header("PlayerComponents")]
        [SerializeField] private RichAI agent;

        //Flags
        private Health health;
        [SerializeField] private float maxSpeed = 6f;
        [HideInInspector] public bool moving;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            agent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<RichAI>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;

            GetComponent<Animator>().SetFloat("forwardSpeed", speed);

            if (speed > 0.1)
            {
                moving = true;
            }
            else
            {
                moving = false;
            }
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        //Set target
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            CancelAttackAnimation();
            agent.destination = destination;
            agent.maxSpeed = maxSpeed * Mathf.Clamp01(speedFraction);
            agent.isStopped = false;
            moving = true;
        }

        private void CancelAttackAnimation()
        { 
            //Esto no deberia ir aca pero no se porque sino no cancela el ataque al moverse
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
        }

        //Stop movement
        public void Cancel()
        {
            agent.isStopped = true;
            moving = false;
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<RichAI>().enabled = false;
            transform.position = position.ToVector();
            GetComponent<RichAI>().enabled = true;
            GetComponent<RichAI>().destination = transform.position;
        }
    }
}
