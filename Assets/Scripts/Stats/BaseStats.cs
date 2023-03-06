using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)] [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startingLevel);
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
        ExperienceReward
    }
}