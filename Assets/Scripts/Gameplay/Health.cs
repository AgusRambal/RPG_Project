using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;

        public bool isPlayer = false;
        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            print(healthPoints);

            if (healthPoints == 0)
            {
                Die();
            }
        }

        public void Die()
        {
            if (isDead)
                return;

            isDead = true;

            GetComponent<Animator>().SetTrigger("Die");
        }
    }
}