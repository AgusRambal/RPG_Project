using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBetweenAttacks = 1f;
    [SerializeField] private float damage = 20f;

    private Health enemyTarget;
    private PlayerController controller;
    private float timeSinceLastAttack = 0;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        MovingToTarget();
    }

    public void MovingToTarget()
    {
        if (enemyTarget == null)
            return;

        if (enemyTarget.IsDead())
        {
            controller.isAttacking = true;
            Invoke("AccessMouseControl", 2.3f);
            controller.StopV2();        
            CancelCombat();
            return;
        }

        if (!GetIsInRange())
        {
            controller.Move(enemyTarget.transform.position);
        }

        else
        {
            controller.Stop();
            AttackBehaviour();
        }
    }

    public void AccessMouseControl()
    {
        controller.SetMoving(false);
    }

    public void AttackBehaviour()
    {
        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            controller.animator.SetTrigger("Attack");
            timeSinceLastAttack = 0;
            //Trigger hit() event
        }
    }

    //Animation Event
    private void Hit()
    {
        enemyTarget.TakeDamage(damage);
    }

    private bool GetIsInRange()
    { 
        return Vector3.Distance(transform.position, enemyTarget.transform.position) < attackRange;
    }

    public bool CanAttack(Health enemy)
    {
        if (enemy == null)
            return false;

        Health target = enemy.GetComponent<Health>();
        return target != null && !target.IsDead();
    }

    //Raycast for clicking the enemy
    public bool Combat()
    {
        RaycastHit[] hits = Physics.RaycastAll(controller.cam.ScreenPointToRay(Input.mousePosition));

        foreach (RaycastHit hit in hits)
        {
            Health target = hit.transform.GetComponent<Health>();

            if (!CanAttack(target))
                continue;

            if (Input.GetMouseButtonDown(1))
            {
                controller.animator.SetBool("isWalking", true);
                enemyTarget = target;
            }

            return true;
        }

        return false;
    }

    public void CancelCombat()
    {
        enemyTarget = null;
    }
}
