using Project.Player;
using System.Collections;
using UnityEngine;

namespace Project.PowerUps
{
    [CreateAssetMenu(fileName = "Magnet Power Up", menuName = "PowerUp/Magnet")]
    public class PowerUp_Magnet : PowerUp
    {
        public float duration;
        public override IEnumerator Use(TruckMovement truck)
        {
            Debug.Log("Magnet");
            Transform parent =  truck.transform.parent;

            yield return new WaitForSeconds(duration);

        }
    }
}
