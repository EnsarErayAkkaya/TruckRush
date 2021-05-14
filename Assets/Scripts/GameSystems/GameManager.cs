using UnityEngine;

namespace Project.GameSystems
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public delegate void OnGameStateChange();
        public OnGameStateChange onGameStart;
        public OnGameStateChange preGameStart;
        public OnGameStateChange onPlayerLost;
        private void Awake()
        {
            instance = this;
        }
        public void PlayerLost()
        {
            Debug.Log("Failure");
            onPlayerLost?.Invoke();
        }
        public void StartGame()
        {
            preGameStart?.Invoke();
            onGameStart?.Invoke();
        }
    }
}
