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
        [SerializeField] private GameObject levelUpButton;
        private PowerUp powerUp;
        private int level;

        public void Set(PowerUp p, int lvl)
        {
            this.title.text = p.name;
            this.icon.sprite = p.icon;
            this.powerUp = p;
            this.level = lvl;

            levelText.text = "Lv." + (lvl < p.powerUpLevelDatas.Count-1? (level + 1).ToString() : "\nMax");
            if(level != powerUp.powerUpLevelDatas.Count - 1)
            {
                requiredCredit.text = powerUp.powerUpLevelDatas[level + 1].requiredLevelCredit.ToString();
            }
            else
            {
                requiredCredit.gameObject.SetActive(false);
            }
            levelUpButton.SetActive(level != powerUp.powerUpLevelDatas.Count - 1);
        }

        public void UpgradePowerUp()
        {
            if (level < powerUp.powerUpLevelDatas.Count - 1 && CreditManager.instance.IsCreditSufficient(powerUp.powerUpLevelDatas[level + 1].requiredLevelCredit))
            {
                CreditManager.instance.LoseCredit(powerUp.powerUpLevelDatas[level + 1].requiredLevelCredit);
                level = PowerUpManager.instance.IncreasePowerUpLevel(powerUp.name);
                levelText.text = "Lv." + (level != powerUp.powerUpLevelDatas.Count - 1 ? (level +1).ToString() : "\nMax");
                if (level != powerUp.powerUpLevelDatas.Count - 1)
                {
                    requiredCredit.text = powerUp.powerUpLevelDatas[level + 1].requiredLevelCredit.ToString();
                }
                else
                {
                    requiredCredit.gameObject.SetActive(false);
                }
                levelUpButton.SetActive(level != powerUp.powerUpLevelDatas.Count - 1);
            }
        }
    }
}