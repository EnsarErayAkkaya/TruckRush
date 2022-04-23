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

        public int coinGained;
        public int coinSpend;

        public static CoinManager instance;

        private void Awake()
        {
            instance = this;
        }

        public void GainCoin(int value)
        {
            coinGained += value;
            coinCount += value;

            onCoinChange?.Invoke(coinCount);
        }
        public void LoseCoin(int value)
        {
            coinSpend += value;
            coinCount -= value;

            //CheckCoinAchivements();

            onCoinChange?.Invoke(coinCount);
        }
        public bool IsCoinSufficient(int requiredValue) => coinCount >= requiredValue;
        
        /* Google play service achivement check
         * public void CheckCoinAchivements()
        {
            if(coinSpend >= 10 && !PlayerPrefs.HasKey("richy_richy") && GooglePlayServicesManager.Instance.isConnectedToGooglePlayServices  )
            {
                PlayerPrefs.SetInt("richy_richy", 1);
                GooglePlayServicesManager.UnlockAchivement(GPGSIds.achievement_richy_richy);
            }
        }*/
    }
}