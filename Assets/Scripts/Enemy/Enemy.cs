using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Enemy : MonoBehaviour
    {
        public GameObject parent;
        public List<GameObject> listOfZombies = new List<GameObject>();
        public List<RuntimeAnimatorController> animator = new List<RuntimeAnimatorController>();
        public List<Avatar> avatar = new List<Avatar>();

        private void Start()
        {
            InstantiateRandomEnemy();
        }

        //Poner una carga de nivel hasta que se terminen de instanciar los zombies
        public void InstantiateRandomEnemy()
        {
            var rnd = Random.Range(0, listOfZombies.Count - 1);

            var instantiatedZombie = Instantiate(listOfZombies[rnd], parent.transform);
            instantiatedZombie.transform.localPosition = Vector3.zero;

            GetComponent<Animator>().runtimeAnimatorController = animator[rnd];
            GetComponent<Animator>().avatar = avatar[rnd];
        }
    }
}