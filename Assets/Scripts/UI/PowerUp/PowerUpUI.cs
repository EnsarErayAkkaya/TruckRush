using Project.GameSystems;
using System;
using UnityEngine;

namespace Project.UI
{
    public class PowerUpUI : MonoBehaviour
    {
        [SerializeField] private Transform powerUpsParent;
        [SerializeField] private GameObject powerUpUIPrefab;

        private void Start()
        {
            CreatePowerUps();
        }

        private void CreatePowerUps()
        {
            /*oreach (var item in PowerUpManager.instance.PowerUps)
            {
                Instantiate(powerUpUIPrefab, powerUpsParent).GetComponent<PowerUpItem>().Set(item);
            }*/
        }
    }
}