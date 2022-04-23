using Project.GameSystems;
using Project.UI;
using Project.UI.Player;
using UnityEngine;

namespace Project.Player
{
    [RequireComponent(typeof(PlayerUI))]
    public class PlayerFuel : MonoBehaviour
    {
        [SerializeField] private TruckMovement truckMovement;
        [SerializeField] private float fuel;
        [SerializeField] private float fuelLoseSpeed;
        
        private GameUI gameUI;
        private float maxFuel;
        private bool useFuel = true;

        private void Start()
        {
            gameUI = FindObjectOfType<GameUI>();
            maxFuel = fuel;
            GameManager.instance.onResurrect += OnResurrect;
        }

        public float SetFuel(float fuel) => this.fuel = fuel;

        private void Update()
        {
            if (truckMovement.CanMove)
            {
                if (useFuel)
                {
                    if (fuel > 0 && fuel <= maxFuel)
                    {
                        fuel -= fuelLoseSpeed * Time.deltaTime;
                    }
                    else
                    {
                        truckMovement.Stop();
                        GameManager.instance.PlayerLost();
                    }
                }
                gameUI.SetFuelFillImage(fuel / maxFuel);
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

        public void OnResurrect()
        {
            GainFuel((int)maxFuel);
        }
    }
}
