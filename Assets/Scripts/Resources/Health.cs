using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;
using RPG.Lazy;
using UnityEngine.Events;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [Header("Unity Events")]
        [SerializeField] private UnityEvent<float> takeDamage;
        [SerializeField] private UnityEvent onDie;

        //Flags
        private LazyValue<float> healthPoints;
        private bool isDead = false;

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        { 
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Start()
        {
            healthPoints.ForceInit();
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);

            if (healthPoints.value == 0)
            {
                onDie.Invoke();
                Die();
                AwardExperience(instigator);
            }

            else
            {
                takeDamage.Invoke(damage);
            }
        }

        public float GetPercentaje()
        {
            return 100 * (healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        public void Die()
        {
            if (isDead)
                return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public void AwardExperience(GameObject instigator)
        {           
            if (!instigator.TryGetComponent<Experience>(out var experience))
                return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        private void RegenerateHealth()
        {
            //Al subir de nivel, rellenamos la vida al maximo
            healthPoints.value = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;

            if (healthPoints.value == 0)
            {
                Die();
            }
        }
    }
}