using UnityEngine;

namespace RPG.Combat
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;

        private Fighter fighter;
        private GameObject player;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                Debug.Log("Chase");
                //fighter.Attack(player);
            }

            else
            {
                //fighter.Cancel();
            }
        }

        private bool InAttackRangeOfPlayer()
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            return distance < chaseDistance;
        }
    }
}