using UnityEngine;

namespace RPG.Combat
{
    public class Magazine : MonoBehaviour
    {
        public int magazineMaxAmount = 0;
        public int ammoLeft;

        [SerializeField] private float reloadTime = 0f;
        [SerializeField] private bool isOnGround = false;

        private Animator owner;

        private void Awake()
        {
            ammoLeft = magazineMaxAmount;

            if (isOnGround)
                return;

            owner = transform.GetComponentInParent<Animator>();      
        }

        //Tengo que agarrar cargadores y repetir esta clase la cantidad de veces cuantos cargadores tenga en el inventario

        public void ResizeMagazine()
        {
            ammoLeft --;

            if (ammoLeft == 0)
            {
                //owner = transform.GetComponentInParent<Animator>();
                owner.GetComponent<Animator>().ResetTrigger("Attack");
                owner.GetComponent<Animator>().SetTrigger("Reload");
                GetComponent<Weapon>().QueueClips();
                Invoke("Reload", reloadTime); //Duracion de la animacion, no lo pude hacer de otra forma por el momento
            }
        }

        public void Reload()
        {
            ammoLeft = magazineMaxAmount;
            owner.GetComponent<Animator>().ResetTrigger("Reload");
            owner.GetComponent<Animator>().SetTrigger("Attack");
        }
    }
}