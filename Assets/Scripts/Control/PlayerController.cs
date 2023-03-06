using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Resources;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        //Flags
        private Health health;
        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead())
                return;

            PlayerRotation();

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
            bool hasHit = Physics.Raycast(GetMouseRay(), out RaycastHit hit);

            if (hasHit)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                return true;
            }

            return false;
        }

        //Player facing the mouse when not moving
        private void PlayerRotation()
        {
            if (Input.GetMouseButton(2))
                return;

            if (GetComponent<Mover>().moving || GetComponent<Fighter>().attacking)
                return;

            transform.LookAt(Input.mousePosition);

            Vector3 positionOnScreen = cam.WorldToViewportPoint(transform.position);
            Vector3 mouseOnScreen = (Vector2)cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = mouseOnScreen - positionOnScreen;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}