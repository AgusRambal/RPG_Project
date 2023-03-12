using RPG.Inventories;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    [RequireComponent(typeof(Pickup))]
    public class ClickablePickup : MonoBehaviour, IRaycastable
    {
        private PlayerController player;
        private Pickup pickup;
        private bool pickUp = false;

        private void Awake()
        {
            pickup = GetComponent<Pickup>();
            player = FindObjectOfType<PlayerController>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        pickUp = true;
                        player.GetComponent<Mover>().MoveTo(transform.position, 1f);
                    }

                    else
                    {
                        pickUp = false;
                    }
                }
            }

            if (pickUp == true)
            {
                PickupItem();
            }
        }

        private void PickupItem()
        {
            var dist = Vector3.Distance(transform.position, player.transform.position);

            if (dist < 1f)
            {
                //player.EquipWeapon(weapon);
                pickup.PickupItem();
                pickUp = false;
            }
        }

        public CursorType GetCursorType()
        {
            if (pickup.CanBePickedUp())
            {
                return CursorType.Pickup;
            }
            else
            {
                return CursorType.FullPickUp;
            }
        }

        public bool HandleRaycast(PlayerController playerController)
        {
            return true;
        }
    }
}