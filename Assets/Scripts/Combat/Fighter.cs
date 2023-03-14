using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Resources;
using RPG.Lazy;
using RPG.Inventories;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [Header("Modifiers")]
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private Transform handTransform = null;
        [SerializeField] private WeaponConfig defaultWeapon = null;

        //Flags
        [HideInInspector] public bool attacking = false;
        [HideInInspector] public bool weaponEquiped = false;
        public Health target;
        private Equipment equipment;
        private float timeSinceLastAttack = Mathf.Infinity;
        public WeaponConfig currentWeaponConfig;
        private LazyValue<Weapon> currentWeapon;
        private GameObject weaponInHands;

        private void Awake()
        {
            currentWeaponConfig = defaultWeapon;
            currentWeapon = new LazyValue<Weapon>(SetupDeafultWeapon);
            equipment = GetComponent<Equipment>();

            if (equipment)
            {
                equipment.equipmentUpdated += UpdateWeapon;
            }
        }

        private Weapon SetupDeafultWeapon()
        {
            return AttachWeapon(defaultWeapon);
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

        public void EquipWeapon(WeaponConfig weapon)
        {
            currentWeaponConfig = weapon;
            currentWeapon.value = AttachWeapon(weapon);
        }

        private void UpdateWeapon()
        {
            var weapon = equipment.GetItemInSlot(EquipLocation.Weapon) as WeaponConfig;

            if (weapon == null)
            {
                EquipWeapon(defaultWeapon);
            }

            else
            {
                weaponEquiped = true;
                EquipWeapon(weapon);
            }
        }

        private Weapon AttachWeapon(WeaponConfig weapon)
        {
            Animator animator = GetComponent<Animator>();
            
            return weapon.Spawn(handTransform, animator);
        }

        public Health GetTarget()
        {
            return target;
        }

        public void AttackBehaviour()
        {
            var magazine = transform.GetComponentInChildren<Magazine>();

            if (magazine != null)
            {
                if (magazine.ammoLeft == 0)
                    return;
            }
              
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

            if (currentWeapon.value != null)
            {
                currentWeapon.value.OnHit();
            }

            if (currentWeaponConfig.HasProjectile())
            {
                Transform weapon = handTransform.Find("WeaponConfig");
                currentWeaponConfig.ShootProjectile(target, weapon.transform.position, gameObject);
                transform.GetComponentInChildren<Magazine>().ResizeMagazine();
            }

            else
            {
                target.TakeDamage(gameObject, currentWeaponConfig.GetDamage());
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
            return Vector3.Distance(transform.position, target.transform.position) < currentWeaponConfig.GetRange();
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
            return currentWeaponConfig.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            WeaponConfig weapon = UnityEngine.Resources.Load<WeaponConfig>(weaponName);
            EquipWeapon(weapon);
        }
    }
}
