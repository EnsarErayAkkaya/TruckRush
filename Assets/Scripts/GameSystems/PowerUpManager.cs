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
        [SerializeField] private TruckMovement truck;
        [SerializeField] private List<PowerUp> allPowerUps;
        [SerializeField] private List<PowerUp> activatedPowerUps;
        [SerializeField] private Dictionary<string, int> powerUpLevels;

        private float powerUpStartingTime;
        private float powerUpLength;
        private PowerUp lastActivePowerUp;
        private bool powerUpEndedNormaly = false;

        private PowerUp selected;

        #region delegates
        public delegate void OnPowerUpUsedOrChange(PowerUp p);
        public OnPowerUpUsedOrChange onPowerUpUsed;
        public OnPowerUpUsedOrChange onPowerUpChange;

        public delegate void OnRemainingTimeChange(float t);
        public OnRemainingTimeChange onRemainingTimeChange;

        public delegate void OnPowerUpEndOrChange();
        public OnPowerUpEndOrChange onPowerUpEnd;
        
        public delegate void OnActivatedPowerUpsChange();
        public OnActivatedPowerUpsChange onActivatedPowerUpsChange;
        #endregion

        public List<PowerUp> AllPowerUps => allPowerUps;
        public List<PowerUp> ActivatedPowerUps => activatedPowerUps;

        public static PowerUpManager instance;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
            powerUpLevels = new Dictionary<string, int>();
            FillDictionary();
            DontDestroyOnLoad(this);
        }
        public void UsePowerUp()
        {
            if (truck == null)
                truck = FindObjectOfType<TruckMovement>();

            ScoreManager.instance.UsedPowerUpCount += 1;

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
            onPowerUpUsed?.Invoke(p);

            float t = p.duration;
            while (t > 0)
            {
                t -= Time.deltaTime;
                p.OnEveryFrame(truck);
                onRemainingTimeChange?.Invoke(t / p.duration);
                yield return null;
            }

            p.OnEnd(truck);
            powerUpEndedNormaly = true;
            onPowerUpEnd?.Invoke();
        }
        public PowerUp GetRandomPowerUp() => activatedPowerUps[Random.Range(0, activatedPowerUps.Count)];
        public void SelectPowerUp(PowerUp p)
        {
            onPowerUpChange?.Invoke(p);
            selected = p;
        }

        #region Entance Power Up Funcitons
        public void SetActivatedPowerUpsLevel()
        {
            foreach (KeyValuePair<string, int> item in powerUpLevels)
            {
                activatedPowerUps.Find(s => s.name == item.Key).SetLevel(item.Value);
            }
        }
        public void FillDictionary()
        {
            foreach (PowerUp item in activatedPowerUps)
            {
                powerUpLevels.Add(item.name, 0);
            }
        }
        public int IncreasePowerUpLevel(string name)
        {
            if (powerUpLevels.ContainsKey(name))
                powerUpLevels[name] += 1;
            else
                powerUpLevels.Add(name, 0);

            activatedPowerUps.Find(s => s.name == name).SetLevel(powerUpLevels[name]);
            return powerUpLevels[name];
        }

        public void ActivatePowerUp(PowerUp p)
        {
            activatedPowerUps.Add(p);
            IncreasePowerUpLevel(p.name);
            onActivatedPowerUpsChange?.Invoke();
        }
        public int PowerUpLevel(string name) => powerUpLevels[name];
        #endregion
    }
}
