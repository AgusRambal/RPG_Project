using TMPro;
using UnityEngine;

namespace RPG.UI
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private TMP_Text damageText = null;

        public void DestroyText()
        { 
            Destroy(gameObject);
        }

        public void SetValue(float amount)
        { 
            damageText.text = $"{amount:0}";
        }
    }
}