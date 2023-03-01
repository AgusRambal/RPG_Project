using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class ChangeScene : MonoBehaviour
    {
        [SerializeField] private float fadeOutTime = 1f;
        [SerializeField] private float fadeInTime = 1f;
        [SerializeField] private float waitTime = 0.5f;

        private int sceneID;

        public void PlayButton(int sceneToLoad)
        {
            sceneID = sceneToLoad;
            StartCoroutine(Transition());
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            fader.GetComponent<Canvas>().sortingOrder = 1;

            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneID);
            yield return new WaitForSeconds(waitTime);
            yield return fader.FadeIn(fadeInTime);

            fader.GetComponent<Canvas>().sortingOrder = -1;
            Destroy(gameObject);
        }
    }
}