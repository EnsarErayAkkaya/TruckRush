using UnityEngine;

namespace Project.GameSystems
{
    public class CoinManager : MonoBehaviour
    {
        private int coinCount;
        public int Coin => coinCount;

        #region event
        public delegate void OnCoinChange(int value);
        public OnCoinChange onCoinChange;
        #endregion

        public static CoinManager instance;

        private void Awake()
        {
            instance = this;
        }

        public void GainCoin(int value)
        {
            coinCount += value;
            onCoinChange?.Invoke(coinCount);
        }
        public void LoseCoin(int value)
        {
            coinCount -= value;
            onCoinChange?.Invoke(coinCount);
        }
        public bool IsCoinSufficient(int requiredValue) => coinCount >= requiredValue;
        

    }
}