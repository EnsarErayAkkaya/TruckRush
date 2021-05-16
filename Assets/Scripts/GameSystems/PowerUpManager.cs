using Project.Player;
using Project.PowerUps;
using Project.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GameSystems
{
    public class PowerUpManager : MonoBehaviour
    {
        [SerializeField] private PowerUpUI powerUpUI;
        [SerializeField] private TruckMovement truck;
        [SerializeField] private List<PowerUp> powerUps;

        private float powerUpStartingTime;
        private float powerUpLength;
        private PowerUp lastActivePowerUp;
        private bool powerUpEndedNormaly = false;

        private PowerUp selected;

        public static PowerUpManager instance;
        private void Awake()
        {
            instance = this;
        }
        public void UsePowerUp()
        {
            if (truck == null)
                truck = FindObjectOfType<TruckMovement>();

            if (powerUpStartingTime + powerUpLength >= Time.time)
            {
                StopAllCoroutines();

                if (!powerUpEndedNormaly)
                {
                    lastActivePowerUp.OnEnd(truck);
                }
            }
            StartCoroutine(EnumeratePowerUp(selected)); // use 
        }
        private IEnumerator EnumeratePowerUp(PowerUp p)
        {
            lastActivePowerUp = p;
            powerUpStartingTime = Time.time;
            powerUpLength = p.duration;
            powerUpEndedNormaly = false;

            p.OnStart(truck);
            powerUpUI.OnPowerUpUsed(p);

            float t = p.duration;
            while (t > 0)
            {
                t -= Time.deltaTime;
                p.OnEveryFrame(truck);
                powerUpUI.SetSliderValue(t / p.duration);
                yield return null;
            }

            p.OnEnd(truck);
            powerUpEndedNormaly = true;
            powerUpUI.OnPowerUpEnd();
        }
        public PowerUp GetRandomPowerUp() => powerUps[Random.Range(0, powerUps.Count)];
        public void SelectPowerUp(PowerUp p)
        {
            powerUpUI.SetPowerUpButton(p);
            selected = p;
        }
    }
}
