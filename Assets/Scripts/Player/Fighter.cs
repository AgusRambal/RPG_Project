using UnityEngine;
using RPG.Movement;
using RPG.Core;
using TreeEditor;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float weaponDamage = 20f;

        private Health target;

        private float timeSinceLastAttack = 0;

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
                return;

            if (target.IsDead())
                return;

            if (!GetIsInRange()) 
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }

            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }

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
            transform.LookAt(target.transform);

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //GetComponent<Animator>().ResetTrigger("StopAttack");
                GetComponent<Animator>().SetTrigger("Attack");
                timeSinceLastAttack = 0;
                //Trigger hit() event
            }
        }

        //Animation Event
        private void Hit()
        {
            if (target == null)
                return;
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
                return false;

            Health target = combatTarget.GetComponent<Health>();
            return target != null && !target.IsDead();
        }

        //Raycast for clicking the enemy
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            //GetComponent<Animator>().ResetTrigger("Attack");
            //GetComponent<Animator>().SetTrigger("StopAttack");
            target = null;
        }
    }
}
