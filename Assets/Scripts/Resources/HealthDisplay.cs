using UnityEngine;
using TMPro;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        private Health health;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            GetComponent<TMP_Text>().text = $"{health.GetPercentaje():0}%";
        }
    }
}