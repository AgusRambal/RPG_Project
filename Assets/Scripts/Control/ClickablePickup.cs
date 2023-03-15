using RPG.Inventories;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    [RequireComponent(typeof(Pickup))]
    public class ClickablePickup : MonoBehaviour, IRaycastable
    {
        private float pickupAnimTime = 2.3f;

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
                        player.pickingItem = true;
                        player.GetComponent<Mover>().MoveTo(transform.position, 1f);
                    }

                    else
                    {
                        pickUp = false;
                    }
                }
            }

            if (pickUp)
            {
                var dist = Vector3.Distance(transform.position, player.transform.position);

                if (dist < 1.3f)
                {
                    player.GetComponent<Mover>().MoveTo(player.transform.position, 1f);
                }
            }

            if (pickUp)
            {
                PickupItem();
            }
        }

        private void PickupItem()
        {
            var dist = Vector3.Distance(transform.position, player.transform.position);

            if (dist < 1.3f)
            {
                player.GetComponent<Animator>().SetTrigger("Pickup");
                player.pickUpSound.Play();
                Invoke("RealPickup", pickupAnimTime);
                pickUp = false;
            }
        }

        public void RealPickup()
        {
            pickup.PickupItem();
            player.pickingItem = false;
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