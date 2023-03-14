using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private UnityEvent onHit;
        [SerializeField] private List<AudioClip> reloadAudioClips;
        [SerializeField] private AudioSource reloadAudioSource;
        [SerializeField] private bool doesntNeedReload = false;

        [HideInInspector] public bool playReload = false;
        private Queue<AudioClip> clipQueue = new Queue<AudioClip>();

        public void OnHit()
        {
            onHit.Invoke();
        }

        void Update()
        {
            if (doesntNeedReload)
                return;

            if (reloadAudioSource.isPlaying == false && clipQueue.Count > 0)
            {
                reloadAudioSource.clip = clipQueue.Dequeue();
                reloadAudioSource.Play();
            }
        }

        public void QueueClips()
        { 
            for (int i = 0; i < reloadAudioClips.Count; i++)
            {
                clipQueue.Enqueue(reloadAudioClips[i]);
            }
        }
    }
}