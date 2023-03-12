using RPG.Control;
using RPG.Movement;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] private WeaponConfig weapon = null;
        [SerializeField] private Fighter player;
        [SerializeField] private UnityEvent takeWeapon;

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
                takeWeapon.Invoke();
                player.EquipWeapon(weapon);
                player.GetComponent<Animator>().SetFloat("AnimationSpeed", weapon.animSpeedMultiplier);
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                Invoke("DestroyGun", 1f);
                pickUp = false;
            }
        }

        public void DestroyGun()
        {
            Destroy(gameObject);
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