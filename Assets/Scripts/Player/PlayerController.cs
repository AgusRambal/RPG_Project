using Pathfinding;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [Header("PlayerComponents")]
        [SerializeField] private RichAI agent;
        [SerializeField] private Animator animator;

        [Header("Dependences")]
        [SerializeField] private Camera cam;

        //Flags
        [HideInInspector] public bool moving;

        private void Update()
        {
            if (InteractWithCombat())
                return;

            if (InteractWithMovement())
                return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null)
                    continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                    continue;

                if (Input.GetMouseButtonDown(1))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }

                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            PlayerRotation();

            if (GetComponent<Mover>().moving)
            {
                MovementActions();
            }

            bool hasHit = Physics.Raycast(GetMouseRay(), out RaycastHit hit);

            if (hasHit)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }

            return false;
        }

        private void MovementActions()
        {
            if (agent.destination != null)
            {
                animator.SetBool("isWalking", true);
                moving = true;
            }

            if (agent.reachedEndOfPath)
            {
                animator.SetBool("isWalking", false);
                moving = false;
            }
        }

        //Player facing the mouse when not moving
        private void PlayerRotation()
        {
            if (Input.GetMouseButton(2) || moving)
                return;

            Vector3 positionOnScreen = cam.WorldToViewportPoint(transform.position);
            Vector3 mouseOnScreen = (Vector2)cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = mouseOnScreen - positionOnScreen;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
            transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}