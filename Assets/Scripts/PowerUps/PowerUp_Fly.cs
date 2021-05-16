using Project.Player;
using System.Collections;
using UnityEngine;

namespace Project.PowerUps
{
    [CreateAssetMenu(fileName = "Fly Power Up", menuName = "PowerUp/Fly")]
    public class PowerUp_Fly : PowerUp
    {
        public float duration;
        public float extraSpeed;

        public override IEnumerator Use(TruckMovement truck)
        {
            Debug.Log("Flying");
            Transform parent =  truck.transform.parent;

            TruckWheelCollision wheelCollision = truck.GetComponent<TruckWheelCollision>();
            PlayerFuel playerFuel = parent.GetComponent<PlayerFuel>();
            TruckAnimation truckAnimation = parent.GetComponent<TruckAnimation>();

            truckAnimation.OpenWings();

            wheelCollision.DontCheckCollision();
            playerFuel.DontUseFuel();
            truck.IncreaseSpeed(extraSpeed);

            yield return new WaitForSeconds(duration);

            wheelCollision.CheckCollision();
            playerFuel.UseFuel();
            truckAnimation.CloseWings();
            truck.DecreaseSpeed(extraSpeed);
            Debug.Log("Flying End");
        }
    }
}
