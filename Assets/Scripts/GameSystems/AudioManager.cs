using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Linq;
using Project.Utility;

namespace Project.GameSystems
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        public Sound[] sounds;
        string[] themes;
        private Sound truckMovementSound;
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }

            gameObject.AddComponent<AudioListener>();

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }

            truckMovementSound = Array.Find(sounds, sound => sound.name == "engineLoop");


            DontDestroyOnLoad(this.gameObject);
        }

        private void Start()
        {
            if (DataManager.instance.savedData.isSoundsOff == true)
                MuteAll();
        }
        public void PlayTheme()
        {
            Sound s = Play(themes[UnityEngine.Random.Range(0, themes.Length)]);
            StartCoroutine(ThemeCountDown(s.source.clip.length));
        }
        public void MuteTheme()
        {
            foreach (var item in themes)
            {
                Mute(item);
            }
        }
        public void UnmuteTheme()
        {

            foreach (var item in themes)
            {
                Unmute(item);
            }
        }
        public Sound Play(string name)
        {
            try
            {
                Sound s = Array.Find(sounds, sound => sound.name == name);
                s.source.Play();
                return s;
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                throw e;
            }
        }
        public Sound Play(int index)
        {
            try
            {
                Sound s = sounds[index];
                s.source.Play();
                return s;
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                throw e;
            }
        }
        public void Stop(string name)
        {
            try
            {
                Sound s = Array.Find(sounds, sound => sound.name == name);
                s.source.Stop();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                throw e;
            }
        }
        public void Stop(int index)
        {
            try
            {
                Sound s = sounds[index];
                s.source.Stop();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                throw e;
            }
        }
        public void Mute(string name)
        {
            try
            {
                Sound s = Array.Find(sounds, sound => sound.name == name);
                s.source.mute = true;
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                throw e;
            }
        }

        public void Unmute(string name)
        {
            try
            {
                Sound s = Array.Find(sounds, sound => sound.name == name);
                s.source.mute = false;
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                throw e;
            }
        }
        public void MuteAll()
        {
            try
            {
                foreach (var item in sounds)
                {
                    item.source.mute = true;
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                throw e;
            }
        }
        public void UnmuteAll()
        {
            try
            {
                foreach (var item in sounds)
                {
                    item.source.mute = false;
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                throw e;
            }
        }
        public bool IsMuted(string name)
        {
            try
            {
                Sound s = Array.Find(sounds, sound => sound.name == name);
                return s.source.mute;
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                throw e;
            }
        }
        IEnumerator ThemeCountDown(float t)
        {
            yield return new WaitForSeconds(t);
            PlayTheme();
        }
        public int GetAudioIndex(string name)
        {
            return Array.FindIndex(sounds, sound => sound.name == name);
        }
        public void ChangeTruckMovementSoundPitch(float percent)
        {
            truckMovementSound.source.pitch = truckMovementSound.minPitch + (truckMovementSound.maxPitch - truckMovementSound.minPitch) * Mathf.Abs(percent);
        }
        public bool IsPlaying(int index) => sounds[index].source.isPlaying;

    }
}