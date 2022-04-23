using Project.Player;
using System.Collections;
using UnityEngine;

namespace Project.PowerUps
{
    [CreateAssetMenu(fileName = "Magnet Power Up", menuName = "PowerUp/Magnet")]
    public class PowerUp_Magnet : PowerUp
    {
        private float _radius;
        public LayerMask coinLayer;
        public string coinTag;
        public float pullSpeed;
        public override void OnEveryFrame(TruckMovement truck)
        {
            if(truck != null)
            {
                Vector3 pos = truck.transform.position;
                Collider[] colliders = Physics.OverlapSphere(pos, _radius, coinLayer);
                foreach (Collider item in colliders)
                {
                    if (item.CompareTag(coinTag))
                    {
                        item.transform.position += (pos - item.transform.position).normalized * pullSpeed;
                    }
                }
            }
        }

        public override void SetLevel(int lvl)
        {
            _duration = powerUpLevelDatas[lvl].powerUpLevelData[0];
            _radius = powerUpLevelDatas[lvl].powerUpLevelData[1];
        }
    }
}
