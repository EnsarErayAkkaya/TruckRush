using UnityEngine;

namespace Project.Utility
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume;
        [Range(-1f, 2f)]
        public float pitch = 1;
        [Range(-1f, 2f)]
        public float minPitch = 1;
        [Range(-1f, 2f)]
        public float maxPitch = 1;
        public bool loop;
        [HideInInspector]
        public AudioSource source;
    }
}