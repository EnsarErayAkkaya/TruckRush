using Project.Player;
using System.Collections;
using UnityEngine;

namespace Project.PowerUps
{
    [CreateAssetMenu(fileName = "Fly Power Up", menuName = "PowerUp/Fly")]
    public class PowerUp_Fly : PowerUp
    {
        public float duration;
        public override IEnumerator Use(TruckMovement truck)
        {
            Debug.Log("Flying");
            Transform parent =  truck.transform.parent;

            TruckWheelCollision wheelCollision = truck.GetComponent<TruckWheelCollision>();
            PlayerFuel playerFuel = parent.GetComponent<PlayerFuel>();

            wheelCollision.DontCheckCollision();
            playerFuel.DontUseFuel();

            yield return new WaitForSeconds(duration);

            wheelCollision.CheckCollision();
            playerFuel.UseFuel();
            Debug.Log("Flying End");
        }
    }
}
