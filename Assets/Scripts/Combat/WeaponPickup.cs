using RPG.Movement;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private WeaponConfig weapon = null;
        [SerializeField] private UnityEvent takeWeapon;

        //Flags
        public Fighter player;
        public bool pickUp = false;
        public float dist;
        private void Awake()
        {
           // player = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Start()
        {
           /* takeWeapon.Invoke();
            player.GetComponent<Animator>().SetFloat("AnimationSpeed", weapon.animSpeedMultiplier);
            player.weaponEquiped = false;*/
        }

        private void Update()
        {
           /* if (Input.GetMouseButtonDown(1))
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
            }*/
        }

        private void Pickup(Fighter player)
        {
           /*  dist = Vector3.Distance(transform.position, player.transform.position);

            if (dist < 1f)
            {
                takeWeapon.Invoke();
                Debug.Log("Asasdadsdsad");

            }*/
        }
    }
}