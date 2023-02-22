using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100f;

    public bool isPlayer = false;
    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
        print(health);
        if (health == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (isPlayer)
        {
            GetComponent<Animator>().SetTrigger("Die");
        }

        else
        {
            GetComponentInChildren<Animator>().SetTrigger("Die");
        }
    }
}
