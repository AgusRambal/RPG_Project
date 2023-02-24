using UnityEngine;

namespace RPG.Combat
{
    public class AIController : MonoBehaviour
    {
        [Header("Modifiers")]
        [SerializeField] private float chaseDistance = 5f;

        //Flags
        private Fighter fighter;
        private GameObject player;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }

        //Testear esto y ver de posicionar bien la camara en la primera
        //SETEAR CORRECTAMENTE TODA LA PARTE DEL CONTROLLER DEL ENEMIGO
        private void Update()
        {
            /*if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                Debug.Log("Chase");
                //fighter.Attack(player);
            }

            else
            {
                //fighter.Cancel();
            }*/
        }

        private bool InAttackRangeOfPlayer()
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            return distance < chaseDistance;
        }
    }
}