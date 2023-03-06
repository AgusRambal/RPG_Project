using RPG.Resources;
using TMPro;
using UnityEngine;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        private Fighter fighter;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            if (fighter.GetTarget() == null)
            {
                GetComponent<TMP_Text>().text = $"N/A";
                return;
            }

            Health health = fighter.GetTarget();
            GetComponent<TMP_Text>().text = $"{health.GetPercentaje():0}%";
        }
    }
}