using TMPro;
using UnityEngine;

namespace RPG.Resources
{
    public class ExperienceDisplay : MonoBehaviour
    {
        private Experience experience;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            GetComponent<TMP_Text>().text = $"{experience.GetPoints()}";
        }
    }
}