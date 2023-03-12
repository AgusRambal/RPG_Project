using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [Tooltip("This prefab will only be spawned once and persisted between " +
        "scenes.")]
        [SerializeField] private GameObject persistentObjectPrefab;

        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned)
                return;

            SpawnPersistanceObject();

            hasSpawned = true;
        }

        private void SpawnPersistanceObject()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}