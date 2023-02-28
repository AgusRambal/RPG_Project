using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [Header("Modifiers")]
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float weaponDamage = 20f;

        //Flags
        public Health target;
        private float timeSinceLastAttack = Mathf.Infinity;

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
                return;

            if (target.IsDead())
            {
                Cancel(); //Esto no deberia ir aca pero no se porque sino no cancela el ataque al matar al enemigo
                return;
            }
            
            if (!GetIsInRange()) 
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }

            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        public void AttackBehaviour()
        {
            transform.LookAt(target.transform);

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                GetComponent<Animator>().ResetTrigger("StopAttack");
                GetComponent<Animator>().SetTrigger("Attack");
                timeSinceLastAttack = 0;
                //Trigger hit() event
            }
        }

        //Animation Event, agregar siempre este evento a la animacion de golpe del enemigo
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
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
            target = null;
            Debug.Log("Cancele pelea");

        }
    }
}
