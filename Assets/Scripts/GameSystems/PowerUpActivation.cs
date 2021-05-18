using Project.PowerUps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project.GameSystems
{
    public class PowerUpActivation : MonoBehaviour
    {
        [SerializeField] private List<PowerUp> allPowerUps;
        [SerializeField] private List<PowerUp> activatedPowerUps;
        public void ActivatePowerUp(PowerUp p)
        {
            activatedPowerUps.Add(p);
        }
    }
}