using RPG.Lazy;
using System;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)] [SerializeField] private int startingLevel = 1;
        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private Progression progression = null;
        [SerializeField] private GameObject levelUpParticleEffect = null;

        public event Action onLevelUp;
        private LazyValue<int> currentLevel;
        private Experience experience;

        private void Awake()
        {
            experience = GetComponent<Experience>();
            currentLevel = new LazyValue<int>(CalculateLevel);
        }

        private void Start()
        {
            currentLevel.ForceInit();        
        }

        private void OnEnable()
        {
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void OnDisable()
        {
            if (experience != null)
            {
                experience.onExperienceGained -= UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel(); 

            if (newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
                LevelUpEfect();
                onLevelUp();
            }
        }

        private void LevelUpEfect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {           
            return currentLevel.value;
        }

        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();

            if (experience == null)
                return startingLevel;

            float curretnXP = GetComponent<Experience>().GetPoints();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);

            for (int i = 1; i <= penultimateLevel; i++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, i);
                if (XPToLevelUp > curretnXP)
                {
                    return i;
                }
            }

            return penultimateLevel + 1;
        }
    }

    public enum CharacterClass
    {
        Player, 
        EnemyUnarmed,
        PistolGuy,
        RifleGuy
    }

    public enum Stat
    { 
        Health,
        ExperienceReward,
        ExperienceToLevelUp
    }
}