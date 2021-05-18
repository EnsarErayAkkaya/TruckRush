using Project.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Project.PowerUps
{
    public abstract class PowerUp : ScriptableObject
    {
        public new string name;
        public Sprite icon;
        public float duration;
        public int requiredActivationCredit;
        public List<PowerUpLevelData> powerUpLevelDatas;

        public virtual void SetLevel(int lvl)
        {
            Debug.Log("implement");
        }

        public virtual void OnEnd(TruckMovement truck)
        {
            Debug.Log("implement");
        }
        public virtual void OnStart(TruckMovement truck)
        {
            Debug.Log("implement");
        }
        public virtual void OnEveryFrame(TruckMovement truck)
        {
        }
    }
}
