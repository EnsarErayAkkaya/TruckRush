using Project.GameSystems;
using Project.PowerUps;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class PowerUpUI : MonoBehaviour
    {
        [SerializeField] private Slider powerUpDurationSlider;
        [SerializeField] private GameObject powerUpButton;
        [SerializeField] private Image powerUpButtonImage;

        public void UsePowerUp()
        {
            powerUpButton.SetActive(false);
            PowerUpManager.instance.UsePowerUp();
        }
        public void SetPowerUpButton(PowerUp p)
        {
            powerUpButtonImage.sprite = p.icon;
            powerUpButton.SetActive(true);
        }

        public void OnPowerUpUsed(PowerUp p)
        {
            Notification.instance.AddNotification(p.name);
            powerUpDurationSlider.gameObject.SetActive(true);
        }
        public void SetSliderValue(float t)
        {
            powerUpDurationSlider.value = t;
        }
        public void OnPowerUpEnd()
        {
            powerUpDurationSlider.gameObject.SetActive(false);
        }
    }
}