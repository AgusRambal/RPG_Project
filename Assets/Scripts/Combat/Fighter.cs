using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Resources;
using RPG.Lazy;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [Header("Modifiers")]
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private Transform handTransform = null;
        [SerializeField] private Weapon defaultWeapon = null;

        //Flags
        [HideInInspector] public bool attacking = false;
        public Health target;
        private float timeSinceLastAttack = Mathf.Infinity;
        private LazyValue<Weapon> currentWeapon;
        
        private void Awake()
        {
            currentWeapon = new LazyValue<Weapon>(SetupDeafultWeapon);
        }

        private Weapon SetupDeafultWeapon()
        {
            AttachWeapon(defaultWeapon);
            return defaultWeapon;
        }

        private void Start()
        {
            currentWeapon.ForceInit();
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
                return;

            if (target.IsDead())
                return;

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

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon.value = weapon;
            AttachWeapon(weapon);
        }

        private void AttachWeapon(Weapon weapon)
        {
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(handTransform, animator);
        }

        public Health GetTarget()
        {
            return target;
        }

        public void AttackBehaviour()
        {
            transform.LookAt(target.transform);

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                GetComponent<Animator>().ResetTrigger("StopAttack");
                GetComponent<Animator>().SetTrigger("Attack");
                timeSinceLastAttack = 0;
                attacking = true;
                //Trigger hit() event
            }
        }

        //Animation Event, agregar siempre este evento a la animacion de golpe del enemigo
        private void Hit()
        {
            if (target == null)
                return;

            if (currentWeapon.value.HasProjectile())
            {
                Transform weapon = handTransform.Find("Weapon");
                currentWeapon.value.ShootProjectile(target, weapon.transform.position, gameObject);
            }

            else
            {
                target.TakeDamage(gameObject, currentWeapon.value.GetDamage());
            }
        }

        //Animation Event, agregar siempre este evento al comienzo de la animacion de Hit
        private void CancelAttack()
        {
            if (target == null)
            {
                Cancel();
                attacking = false;
            }

            else if (target.IsDead())
            {
                Cancel();
                attacking = false;
            }
        }
       
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.value.GetRange();
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
        }

        public object CaptureState()
        {
            return currentWeapon.value.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
    }
}
