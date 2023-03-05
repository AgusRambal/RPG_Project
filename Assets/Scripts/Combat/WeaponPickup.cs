using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private Weapon weapon = null;
        [SerializeField] private Fighter player;

        private bool pickUp;

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
                    }

                    else
                    {
                        pickUp = false;
                    }
                }
            }

            if (pickUp == true)
            {
                var dist = Vector3.Distance(transform.position, player.transform.position);

                if (dist < 1f)
                {                   
                    Destroy(player.gun);
                    player.EquipWeapon(weapon);
                    player.GetComponent<Animator>().SetFloat("AnimationSpeed", weapon.animSpeedMultiplier);
                    Destroy(gameObject);
                }
            }
        }
    }
}