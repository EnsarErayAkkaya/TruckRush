using Project.GameSystems;
using Project.PowerUps;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class ActivatedPowerUpUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI requiredCredit;
        [SerializeField] private Image icon;
        private PowerUp powerUp;
        private int level;

        public void Set(PowerUp p, int lvl)
        {
            title.text = p.name;
            icon.sprite = p.icon;
            this.powerUp = p;
            Debug.Log("power up " + powerUp.name + ", level :" + lvl);
            levelText.text = lvl < p.powerUpLevelDatas.Count-1? lvl.ToString() : "Max";
            requiredCredit.text = level != powerUp.powerUpLevelDatas.Count - 1 ? powerUp.powerUpLevelDatas[level + 1].requiredLevelCredit.ToString() : "";
            level = lvl;
        }

        public void UpgradePowerUp()
        {
            if (level < powerUp.powerUpLevelDatas.Count - 1 && CreditManager.instance.IsCreditSufficient(powerUp.powerUpLevelDatas[level + 1].requiredLevelCredit))
            {
                CreditManager.instance.LoseCredit(powerUp.powerUpLevelDatas[level + 1].requiredLevelCredit);
                level = PowerUpManager.instance.IncreasePowerUpLevel(powerUp.name);
                levelText.text = level != powerUp.powerUpLevelDatas.Count - 1 ? level.ToString() : "Max";
                requiredCredit.text = level != powerUp.powerUpLevelDatas.Count - 1 ? powerUp.powerUpLevelDatas[level + 1].requiredLevelCredit.ToString() : "";
            }
        }
    }
}