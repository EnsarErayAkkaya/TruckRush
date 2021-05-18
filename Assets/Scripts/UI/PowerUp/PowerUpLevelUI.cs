using Project.GameSystems;
using Project.PowerUps;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Project.UI
{
    public class PowerUpLevelUI : MonoBehaviour
    {
        [SerializeField] private GameObject activatedPowerUpPrefab;
        [SerializeField] private GameObject notActivatedPowerUpPrefab;
        [SerializeField] private Transform powerUpParent;

        private void Start()
        {
            CreatePowerUpItems();
            PowerUpManager.instance.onActivatedPowerUpsChange += CreatePowerUpItems;
        }
        private void OnDestroy()
        {
            PowerUpManager.instance.onActivatedPowerUpsChange -= CreatePowerUpItems;
        }
        public void CreatePowerUpItems()
        {
            foreach (Transform item in powerUpParent)
            {
                Destroy(item.gameObject);
            }

            List<PowerUp> notActivatedPowerUps = PowerUpManager.instance.AllPowerUps
                .Except(PowerUpManager.instance.ActivatedPowerUps).ToList();

            foreach (var item in PowerUpManager.instance.ActivatedPowerUps)
            {
                Instantiate(activatedPowerUpPrefab, powerUpParent)
                    .GetComponent<ActivatedPowerUpUI>().Set(item, PowerUpManager.instance.PowerUpLevel(item.name));
            }
            foreach (var item in notActivatedPowerUps)
            {
                Instantiate(notActivatedPowerUpPrefab, powerUpParent)
                    .GetComponent<NotActivatedPowerUpUI>().Set(item);
            }
        }
    }
}