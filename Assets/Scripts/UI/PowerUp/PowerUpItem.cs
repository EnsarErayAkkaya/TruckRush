using Project.GameSystems;
using Project.PowerUps;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class PowerUpItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Image icon;
        [SerializeField] private Transform powerUpRow;
        [SerializeField] private GameObject powerUpCounterPrefab;

        public void Set(PowerUp p)
        {
            title.text = p.name;
            icon.sprite = p.icon;
            /*for (int i = 0; i < PowerUpManager.instance.PowerUpCount(p.name); i++)
            {
                Instantiate(powerUpCounterPrefab, powerUpRow);
            }*/
        }

        /*public void BuyPowerUp()
        {
            if (PowerUpManager.instance.PowerUpCount(title.text) < 30)
            {
                PowerUpManager.instance.BuyPowerUp(title.text);
                Instantiate(powerUpCounterPrefab, powerUpRow);
            }
        }*/
    }
}