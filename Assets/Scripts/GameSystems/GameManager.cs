using System.Collections;
using UnityEngine;

namespace Project.GameSystems
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float engineStartDuration;

        public static GameManager instance;

        public delegate void OnGameStateChange();
        public OnGameStateChange onGameStart;
        public OnGameStateChange preGameStart;
        public OnGameStateChange onPlayerLost;
        public OnGameStateChange onResurrect;

        public bool resurrected;
        private void Awake()
        {
            instance = this;
            TruckManager.instance.CreateTruck();
            
        }
        private void Start()
        {
            if (DataManager.instance.savedData.isPlayedBefore == false)
            {
                DataManager.instance.savedData.isPlayedBefore = true;
                DataManager.instance.Save();
            }
        }
        public void PlayerLost()
        {
            Debug.Log("Failure");
            AudioManager.instance.Stop("engineLoop");
            onPlayerLost?.Invoke();
        }
        public void StartGame()
        {
            preGameStart?.Invoke();
            onGameStart?.Invoke();
            AudioManager.instance.Play("engineLoop");
        }
        public void Resurrect()
        {
            if(!resurrected)
            {
                TruckManager.instance.TransportTruckToClosestRoad();
                resurrected = true;
                onResurrect?.Invoke();
            }
        }
    }
}
