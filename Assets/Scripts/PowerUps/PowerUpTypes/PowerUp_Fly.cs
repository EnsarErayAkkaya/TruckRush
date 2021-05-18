using Project.Player;
using System.Collections;
using UnityEngine;

namespace Project.PowerUps
{
    [CreateAssetMenu(fileName = "Fly Power Up", menuName = "PowerUp/Fly")]
    public class PowerUp_Fly : PowerUp
    {
        public float extraSpeed;
        private TruckWheelCollision wheelCollision;
        private PlayerFuel playerFuel;
        private TruckAnimation truckAnimation;

        public override void OnStart(TruckMovement truck)
        {
            Debug.Log("Flying Start");
            Transform parent =  truck.transform.parent;

            if(wheelCollision == null)
                wheelCollision = truck.GetComponent<TruckWheelCollision>();
            if(playerFuel == null)
                playerFuel = parent.GetComponent<PlayerFuel>();
            if (truckAnimation == null)
                truckAnimation = parent.GetComponent<TruckAnimation>();

            truckAnimation.OpenWings();

            wheelCollision.DontCheckCollision();
            playerFuel.DontUseFuel();
            truck.IncreaseSpeed(extraSpeed);
        }
        public override void OnEnd(TruckMovement truck)
        {
            Transform parent = truck.transform.parent;

            if (wheelCollision == null)
                wheelCollision = truck.GetComponent<TruckWheelCollision>();
            if (playerFuel == null)
                playerFuel = parent.GetComponent<PlayerFuel>();
            if (truckAnimation == null)
                truckAnimation = parent.GetComponent<TruckAnimation>();

            truck.DecreaseSpeed(extraSpeed);
            truckAnimation.CloseWings();
            wheelCollision.CheckCollision();
            playerFuel.UseFuel();
            Debug.Log("Flying End");
        }
        public override void SetLevel(int lvl)
        {
            duration = powerUpLevelDatas[lvl].powerUpLevelData[0];
        }
    }
}
