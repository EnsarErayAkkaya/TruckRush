using Project.Player;
using System.Collections;
using UnityEngine;

namespace Project.PowerUps
{
    [CreateAssetMenu(fileName = "Magnet Power Up", menuName = "PowerUp/Magnet")]
    public class PowerUp_Magnet : PowerUp
    {
        public float radius;
        public LayerMask coinLayer;
        public string coinTag;
        public float pullSpeed;

        public override void OnStart(TruckMovement truck)
        {
            Debug.Log("Magnet");
        }
        public override void OnEveryFrame(TruckMovement truck)
        {
            if(truck != null)
            {
                Vector3 pos = truck.transform.position;
                Collider[] colliders = Physics.OverlapSphere(pos, radius, coinLayer);
                foreach (Collider item in colliders)
                {
                    if (item.CompareTag(coinTag))
                    {
                        item.transform.position += (pos - item.transform.position).normalized * pullSpeed;
                    }
                }
            }
        }
        public override void OnEnd(TruckMovement truck)
        {
            Debug.Log("Magnet End");
        }
    }
}
