using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F6))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                Save();
            }
        }

        private void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        private void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }
}