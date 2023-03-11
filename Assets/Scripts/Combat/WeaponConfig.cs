using RPG.Resources;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "RPG.NewWeapon")]
    public class WeaponConfig : ScriptableObject
    {
        public Weapon prefabToEquip;
        public AnimatorOverrideController animatorOverride;
        public float weaponDamage;
        public float weaponRange;
        public Projectile projectile = null;
        public float animSpeedMultiplier = 1f;

        const string weaponName = "WeaponConfig";

        public Weapon Spawn(Transform handTransform, Animator animator)
        {
            DestroyOldWeapon(handTransform);

            Weapon weapon = null;

            if (prefabToEquip != null)
            {
                weapon = Instantiate(prefabToEquip, handTransform);
                weapon.gameObject.name = weaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }

            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }

            return weapon;
        }

        private void DestroyOldWeapon(Transform handTransform)
        {
            Transform oldWeapon = handTransform.Find(weaponName);
            if (oldWeapon == null)
                return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void ShootProjectile(Health target, Vector3 gun, GameObject instigator)
        {
            Projectile projectileInstance = Instantiate(projectile, gun, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, weaponDamage);
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetRange()
        {
            return weaponRange;
        }
    }
}