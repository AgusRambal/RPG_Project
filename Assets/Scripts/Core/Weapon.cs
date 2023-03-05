using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG.NewWeapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        public GameObject prefabToEquip;
        public AnimatorOverrideController animatorOverride;
        public float weaponDamage;
        public float weaponRange;
        public Projectile projectile = null;
        public float animSpeedMultiplier = 1f;

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (prefabToEquip != null)
            {
               Instantiate(prefabToEquip, handTransform);
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
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void ShootProjectile(Health target, Vector3 gun)
        {
            Projectile projectileInstance = Instantiate(projectile, gun, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
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