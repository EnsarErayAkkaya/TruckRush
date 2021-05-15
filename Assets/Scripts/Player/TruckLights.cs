using Project.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class TruckLights : MonoBehaviour
    {
        [SerializeField] private GameObject leftHeadlight;
        [SerializeField] private GameObject rightHeadlight;

        private void Start()
        {
            DayNightManager.instance.onNight += OpenLights;
            DayNightManager.instance.onDay += CloseLights;
        }

        public void OpenLights()
        {
            leftHeadlight.gameObject.SetActive(true);
            rightHeadlight.gameObject.SetActive(true);
        }
        public void CloseLights()
        {
            leftHeadlight.gameObject.SetActive(false);
            rightHeadlight.gameObject.SetActive(false);
        }
    }
}