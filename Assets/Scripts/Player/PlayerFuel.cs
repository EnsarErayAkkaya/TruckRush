using Project.GameSystems;
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

        private bool useFuel = true;
        private void Start()
        {
            maxFuel = fuel;
        }

        public float SetFuel(float fuel) => this.fuel = fuel;

        private void Update()
        {
            if (truckMovement.CanMove && useFuel)
            {
                if (fuel > 0 && fuel <= maxFuel)
                {
                    fuel -= fuelLoseSpeed * Time.deltaTime;
                    playerUI.SetFuelFillImage(fuel / maxFuel);
                }
                else
                {
                    truckMovement.Stop();
                    GameManager.instance.PlayerLost();
                }
            }
        }
        public void GainFuel(int value)
        {
            fuel += value;
            if (fuel > maxFuel)
                fuel = maxFuel;
        }
        public void DontUseFuel() => useFuel = false;
        public void UseFuel() => useFuel = true;
    }
}
