using Project.Player;
using System.Collections;
using UnityEngine;

namespace Project.PowerUps
{
    [CreateAssetMenu(fileName = "Shield Power Up", menuName = "PowerUp/Shield")]
    public class PowerUp_Shield : PowerUp
    {
        PlayerHealth playerHealth;
        TruckAnimation truckAnimation;

        public override void OnStart(TruckMovement truck)
        {
            Debug.Log("Shield");
            Transform parent = truck.transform.parent;

            if(playerHealth == null)
                playerHealth = parent.GetComponent<PlayerHealth>();
            if(truckAnimation == null)
                truckAnimation = parent.GetComponent<TruckAnimation>();

            truckAnimation.OpenShields();
            playerHealth.CantGetDamage();
        }
        public override void OnEnd(TruckMovement truck)
        {
            Transform parent = truck.transform.parent;
            if (playerHealth == null)
                playerHealth = parent.GetComponent<PlayerHealth>();
            if (truckAnimation == null)
                truckAnimation = parent.GetComponent<TruckAnimation>();

            truckAnimation.CloseShields();
            playerHealth.CanGetDamage();

            Debug.Log("Shield End");
        }
    }
}
