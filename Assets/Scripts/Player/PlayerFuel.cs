using Project.UI.Player;
using UnityEngine;

namespace Project.Player
{
    [RequireComponent(typeof(PlayerUI))]
    public class PlayerFuel : MonoBehaviour
    {
        [SerializeField] private PlayerUI playerUI;
        [SerializeField] private TruckMovement truckMovement;
        [SerializeField] private float fuel;
        private float maxFuel;
        [SerializeField] private float fuelLoseSpeed;
        private void Start()
        {
            maxFuel = fuel;
        }

        public float SetFuel(float fuel) => this.fuel = fuel;

        private void Update()
        {
            if (fuel > 0 && fuel <= maxFuel) 
            { 
                fuel -= fuelLoseSpeed * Time.deltaTime;
                playerUI.SetFuelFillImage(fuel / maxFuel);
            }
            else if(truckMovement.CanMove)
            {
                truckMovement.Stop();
            }
        }
        public void GainFuel(int value)
        {
            fuel += value;
            if (fuel > maxFuel)
                fuel = maxFuel;
        }
    }
}
