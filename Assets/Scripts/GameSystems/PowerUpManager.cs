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
        public Dictionary<string, int> PowerUpLevels => powerUpLevels;

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
            DontDestroyOnLoad(this);
            GetSavedData();
        }
        private void Start()
        {
            SetActivatedPowerUpsLevel();
        }
        private void StopUsingPowerUp()
        {
            if (powerUpStartingTime + powerUpLength >= Time.time)
            {
                StopAllCoroutines();

                if (!powerUpEndedNormaly)
                {
                    lastActivePowerUp.OnEnd(truck);
                }
            }
        }
        public void UsePowerUp()
        {
            if (truck == null)
                truck = FindObjectOfType<TruckMovement>();

            ScoreManager.instance.UsedPowerUpCount += 1;

            StopUsingPowerUp();

            StartCoroutine(EnumeratePowerUp(selected)); // use 
        }
        public void UsePowerUp(string powerUpName)
        {
            if (truck == null)
                truck = FindObjectOfType<TruckMovement>();

            ScoreManager.instance.UsedPowerUpCount += 1;

            StopUsingPowerUp();

            StartCoroutine(EnumeratePowerUp(allPowerUps.Find(p => p.name == powerUpName))); // use 
        }

        private IEnumerator EnumeratePowerUp(PowerUp p)
        {
            lastActivePowerUp = p;
            powerUpStartingTime = Time.time;
            powerUpLength = p.Duration;
            powerUpEndedNormaly = false;

            p.OnStart(truck);
            onPowerUpUsed?.Invoke(p);

            float t = p.Duration;
            while (t > 0)
            {
                t -= Time.deltaTime;
                p.OnEveryFrame(truck);
                onRemainingTimeChange?.Invoke(t / p.Duration);
                yield return null;
            }

            p.OnEnd(truck);
            powerUpEndedNormaly = true;
            onPowerUpEnd?.Invoke();
        }
        public PowerUp GetRandomPowerUp() => activatedPowerUps.Count > 0 ? 
            activatedPowerUps[Random.Range(0, activatedPowerUps.Count)] : null;
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
        public void GetSavedData()
        {
            powerUpLevels = new Dictionary<string, int>();
            for (int i = 0; i < DataManager.instance.savedData.powerUpLevelNames.Count; i++)
            {
                string key = DataManager.instance.savedData.powerUpLevelNames[i];
                int lvl = DataManager.instance.savedData.powerUpLevels[i];
                powerUpLevels.Add(key, lvl);
            }
            activatedPowerUps = new List<PowerUp>();

            foreach (string item in powerUpLevels.Keys)
            {
                activatedPowerUps.Add(allPowerUps.Find(s => s.name == item));
            }
        }
        public int IncreasePowerUpLevel(string name)
        {
            if (powerUpLevels.ContainsKey(name))
                powerUpLevels[name] += 1;
            else
                powerUpLevels.Add(name, 0);

            activatedPowerUps.Find(s => s.name == name).SetLevel(powerUpLevels[name]);

            /// Saving Power Up
            List<int> levels = new List<int>();
            foreach (int lvl in powerUpLevels.Values)
            {
                levels.Add(lvl);
            }
            List<string> names = new List<string>();
            foreach (string pName in powerUpLevels.Keys)
            {
                names.Add(pName);
            }

            DataManager.instance.savedData.powerUpLevels = levels;
            DataManager.instance.savedData.powerUpLevelNames = names;
            DataManager.instance.Save();
            /// Saving power up
            
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
