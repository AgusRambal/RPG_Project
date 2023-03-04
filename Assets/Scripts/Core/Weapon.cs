using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG.NewWeapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private GameObject prefabToEquip;
        [SerializeField] private AnimatorOverrideController animatorOverride;
        [SerializeField] private float weaponDamage;
        [SerializeField] private float weaponRange;

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (prefabToEquip != null)
            {
                Instantiate(prefabToEquip, handTransform);
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
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