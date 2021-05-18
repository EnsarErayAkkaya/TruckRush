using Project.GameSystems;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.UI
{
    public class CreditUI : MonoBehaviour
    {
        public TextMeshProUGUI creditText;
        private void Start()
        {
            CreditManager.instance.onCreditChange += SetCreditText;
            SetCreditText(CreditManager.instance.Credit);
        }
        private void OnDestroy()
        {
            CreditManager.instance.onCreditChange -= SetCreditText;
        }
        public void SetCreditText(int c)
        {
            creditText.text = c.ToString();
        }
    }
}