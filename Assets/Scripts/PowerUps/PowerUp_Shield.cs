using Project.Player;
using System.Collections;
using UnityEngine;

namespace Project.PowerUps
{
    [CreateAssetMenu(fileName = "Shield Power Up", menuName = "PowerUp/Shield")]
    public class PowerUp_Shield : PowerUp
    {
        public float duration;
        public override IEnumerator Use(TruckMovement truck)
        {
            Debug.Log("Shield");
            Transform parent = truck.transform.parent;

            PlayerHealth playerHealth = parent.GetComponent<PlayerHealth>();

            playerHealth.CantGetDamage();

            yield return new WaitForSeconds(duration);

            Debug.Log("Shield End");
            playerHealth.CanGetDamage();
        }
    }
}
