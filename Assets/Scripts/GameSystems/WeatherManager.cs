using Project.Player;
using System;
using System.Collections;
using UnityEngine;

namespace Project.GameSystems
{
    public class WeatherManager : MonoBehaviour
    {
        public enum Weather
        {
            Cloudy, Rainy, Snowy, Sunny
        }


        [SerializeField] private float particleHeight;
        [SerializeField] private float minWeatherDuration;
        [SerializeField] private float maxWeatherDuration;

        [SerializeField] private Vector3 offset;

        [SerializeField] private ParticleSystem rainParticle;
        [SerializeField] private ParticleSystem snowParticle;
        
        private Transform truck;
        private Weather currentWeather;
        private Weather lastWeather;
        private ParticleSystem currentParticle;
        private ParticleSystem lastParticle;
        private int timeMultiplier = 1;

        private void Start()
        {
            truck = FindObjectOfType<TruckMovement>().transform;
            GameManager.instance.onPlayerLost += StopCurrentWeatherSound;
            GameManager.instance.onResurrect += PlayCurrentWeatherSound;
            ChangeWeather();
        }
        /// <summary>
        /// Change Weather Randomly
        /// </summary>
        private void ChangeWeather()
        {
            lastWeather = currentWeather;
            currentWeather = (Weather)(UnityEngine.Random.Range(0, Enum.GetNames(typeof(Weather)).Length));
            SetWeather();
            StartCoroutine(ChangeWeatherEnumerator());
        }

        private IEnumerator ChangeWeatherEnumerator()
        {
            float target = UnityEngine.Random.Range(minWeatherDuration, maxWeatherDuration);
            float t = 0;
            while(t < target)
            {
                t += Time.deltaTime * timeMultiplier;

                yield return null;
            }
            ChangeWeather();
        }

        /// <summary>
        /// Set Weather particles, do invokes here if neccessary
        /// </summary>
        private void SetWeather()
        {
            if(lastWeather != currentWeather)
            {
                StopLastWeatherSound();

                if (currentParticle != null)
                {
                    lastParticle = currentParticle;
                    currentParticle.Stop();
                    StartCoroutine(DeactivateLastParticle());
                }

                switch (currentWeather)
                {
                    case Weather.Cloudy:
                        currentParticle = null;
                        break;
                    case Weather.Rainy:
                        AudioManager.instance.Play("rain");
                        currentParticle = rainParticle;
                        break;
                    case Weather.Snowy:
                        AudioManager.instance.Play("snow");
                        currentParticle = snowParticle;
                        break;
                    case Weather.Sunny:
                        currentParticle = null;
                        break;
                    default:
                        break;
                }
                if (currentParticle != null)
                {
                    currentParticle.gameObject.SetActive(true);
                    currentParticle.Play();
                }
            }
        }

        private void LateUpdate()
        {
            if (currentParticle != null)
            {
                currentParticle.transform.position = new Vector3(truck.position.x, particleHeight, truck.position.z) + offset;
            }
        }

        private void PlayCurrentWeatherSound()
        {
            timeMultiplier = 1;
            switch (currentWeather)
            {
                case Weather.Cloudy:
                    break;
                case Weather.Rainy:
                    AudioManager.instance.Play("rain");
                    break;
                case Weather.Snowy:
                    AudioManager.instance.Play("snow");
                    break;
                case Weather.Sunny:
                    break;
                default:
                    break;
            }
        }

        private void StopLastWeatherSound()
        {
            switch (lastWeather)
            {
                case Weather.Cloudy:
                    break;
                case Weather.Rainy:
                    AudioManager.instance.Stop("rain");
                    break;
                case Weather.Snowy:
                    AudioManager.instance.Stop("snow");
                    break;
                case Weather.Sunny:
                    break;
                default:
                    break;
            }
        }
        private void StopCurrentWeatherSound()
        {
            timeMultiplier = 0;
            switch (currentWeather)
            {
                case Weather.Cloudy:
                    break;
                case Weather.Rainy:
                    AudioManager.instance.Stop("rain");
                    break;
                case Weather.Snowy:
                    AudioManager.instance.Stop("snow");
                    break;
                case Weather.Sunny:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Deactivate ParticleSystem when all particles died
        /// </summary>
        private IEnumerator DeactivateLastParticle()
        {
            while(lastParticle.particleCount > 0)
            {
                yield return new WaitForSeconds(1);
            }
            lastParticle.gameObject.SetActive(false);
        }
    }
}
