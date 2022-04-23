using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.GameSystems
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private Text truckLayoutSettingButton;
        [SerializeField] private Text soundSettingsButton;



        private void Start()
        {
            if (DataManager.instance.savedData.isTruckLayoutHidden)
            {
                truckLayoutSettingButton.text = "Show Truck Layout";
            }
            else
            {
                truckLayoutSettingButton.text = "Hide Truck Layout";
            }

            if (DataManager.instance.savedData.isSoundsOff)
            {
                soundSettingsButton.text = "Turn Sounds On";
            }
            else
            {
                soundSettingsButton.text = "Turn Sounds Off";
            }
        }

        public void OnTruckLayoutSettingButton()
        {
            if(DataManager.instance.savedData.isTruckLayoutHidden)
            {
                DataManager.instance.savedData.isTruckLayoutHidden = false;
                truckLayoutSettingButton.text = "Hide Truck Layout";
            }
            else
            {
                DataManager.instance.savedData.isTruckLayoutHidden = true;
                truckLayoutSettingButton.text = "Show Truck Layout";
            }
            DataManager.instance.Save();
        }

        public void OnSoundsSettingButton()
        {
            if (DataManager.instance.savedData.isSoundsOff)
            {
                DataManager.instance.savedData.isSoundsOff = false;
                soundSettingsButton.text = "Turn Sounds Off";
                AudioManager.instance.UnmuteAll();
            }
            else
            {
                DataManager.instance.savedData.isSoundsOff = true;
                soundSettingsButton.text = "Turn Sounds On";
                AudioManager.instance.MuteAll();
            }
            DataManager.instance.Save();
        }
        public void OnSettingsButton()
        {
            if(settingsPanel.activeInHierarchy)
            {
                settingsPanel.gameObject.SetActive(false);
            }
            else
            {
                settingsPanel.gameObject.SetActive(true);
            }
        }
    }
}