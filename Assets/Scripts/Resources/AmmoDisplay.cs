using TMPro;
using UnityEngine;

namespace RPG.Combat
{
    public class AmmoDisplay : MonoBehaviour
    {
        private Magazine magazine;

        private void Update()
        {
            magazine = GameObject.FindWithTag("Player").GetComponent<Fighter>().transform.GetComponentInChildren<Magazine>();

            if (magazine == null)
            {
                GetComponent<TMP_Text>().text = $"0/0";
                return;
            }

            GetComponent<TMP_Text>().text = $"{magazine.ammoLeft}/{magazine.magazineMaxAmount}";
        }
    }
}
