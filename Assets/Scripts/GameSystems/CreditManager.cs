using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GameSystems
{
    public class CreditManager : MonoBehaviour
    {
        private int creditCount;
        [SerializeField] private int powerUpCreditMultiplier;
        [SerializeField] private int milestoneCreditMultiplier;

        #region event
        public delegate void OnCreditChange(int value);
        public OnCreditChange onCreditChange;
        #endregion
        public int Credit => creditCount;
        public int PowerUpCreditMultiplier => powerUpCreditMultiplier;
        public int MilestoneCreditMultiplier => milestoneCreditMultiplier;

        public static CreditManager instance;

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
        }
        private void Start()
        {
            creditCount = DataManager.instance.savedData.credit;
            onCreditChange?.Invoke(creditCount);
        }
        public void DoubleGainedCredit()
        {
            creditCount += ScoreManager.instance.GainedCredit;
            DataManager.instance.savedData.credit = creditCount;
            DataManager.instance.Save();
        }

        public void GainCredit(int value)
        {
            creditCount += value;
            onCreditChange?.Invoke(creditCount);
            DataManager.instance.savedData.credit = creditCount;
            DataManager.instance.Save();
        }
        public void LoseCredit(int value)
        {
            creditCount -= value;
            onCreditChange?.Invoke(creditCount);
            DataManager.instance.savedData.credit = creditCount;
            DataManager.instance.Save();
        }
        public bool IsCreditSufficient(int requiredValue) 
        {
            if (creditCount >= requiredValue)
                return true;
            Debug.Log("UnSefficient Money");
            return false;
        }
    }
}