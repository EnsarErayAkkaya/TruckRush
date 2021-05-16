using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GameSystems
{
    public class CreditManager : MonoBehaviour
    {
        private int creditCount;
        public int Credit => creditCount;

        #region event
        public delegate void OnCreditChange(int value);
        public OnCreditChange onCreditChange;
        #endregion

        public static CreditManager instance;

        private void Awake()
        {
            if(instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(this);
        }

        public void GainCredit(int value)
        {
            creditCount += value;
            onCreditChange?.Invoke(creditCount);
        }
        public void LoseCredit(int value)
        {
            creditCount -= value;
            onCreditChange?.Invoke(creditCount);
        }
        public bool IsCreditSufficient(int requiredValue) => creditCount >= requiredValue;
    }
}