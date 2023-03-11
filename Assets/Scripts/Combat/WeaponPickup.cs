using RPG.Control;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] private Weapon weapon = null;
        [SerializeField] private Fighter player;

        private bool pickUp = false;

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
                Pickup(player);
            }
        }

        private void Pickup(Fighter player)
        {
            var dist = Vector3.Distance(transform.position, player.transform.position);

            if (dist < 1f)
            {
                player.EquipWeapon(weapon);
                player.GetComponent<Animator>().SetFloat("AnimationSpeed", weapon.animSpeedMultiplier);
                Destroy(gameObject);
            }
        }

        public bool HandleRaycast(PlayerController playerController)
        {  
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }
}