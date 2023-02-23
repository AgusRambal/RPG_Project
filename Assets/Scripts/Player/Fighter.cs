using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float damage = 20f;

        private Transform target;

        //private Health enemyTarget;
        // private Mover controller;
        private float timeSinceLastAttack = 0;

        private void Awake()
        {
            //controller = GetComponent<Mover>();
        }

        void Update()
        {
            if (target == null)
                return;

            if (!GetIsInRange()) 
            {
                GetComponent<Mover>().MoveTo(target.position);
            }

            else
            {
                GetComponent<Mover>().Cancel();
            }

            //timeSinceLastAttack += Time.deltaTime;

            //MovingToTarget();
        }

        public void MovingToTarget()
        {
           // if (enemyTarget == null)
            //    return;

          /*  if (enemyTarget.IsDead())
            {
                //  controller.isAttacking = true;
                Invoke("AccessMouseControl", 2.3f);
                // controller.StopV2();        
                CancelCombat();
                return;
            }*/

            /*if (!GetIsInRange())
            {
                 controller.Move(enemyTarget.transform.position);
            }*/

           /* else
            {
                controller.Stop();
                AttackBehaviour();
            }*/
        }

        public void AccessMouseControl()
        {
            // controller.SetMoving(false);
        }

        public void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //controller.animator.SetTrigger("Attack");
                timeSinceLastAttack = 0;
                //Trigger hit() event
            }
        }

        //Animation Event
        private void Hit()
        {
           // enemyTarget.TakeDamage(damage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(Health enemy)
        {
            if (enemy == null)
                return false;

            Health target = enemy.GetComponent<Health>();
            return target != null && !target.IsDead();
        }

        //Raycast for clicking the enemy
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}
