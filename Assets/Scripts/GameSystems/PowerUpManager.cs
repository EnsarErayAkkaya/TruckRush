using Project.Player;
using Project.PowerUps;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GameSystems
{
    public class PowerUpManager : MonoBehaviour
    {
        [SerializeField] private TruckMovement truck;

        [SerializeField] private List<PowerUp> powerUps;

        public static PowerUpManager instance;
        private void Awake()
        {
            instance = this;
        }
        public void UsePowerUp(string name)
        {
            Debug.Log(name);
            if (truck == null)
                truck = FindObjectOfType<TruckMovement>();

            StartCoroutine(powerUps.Find(s => s.name == name).Use(truck)); // use            
        }
        public string GetRandomPowerUp() => powerUps[Random.Range(0, powerUps.Count)].name;
    }
}
