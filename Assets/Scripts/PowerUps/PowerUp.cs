using Project.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Project.PowerUps
{
    [System.Serializable]
    public abstract class PowerUp : ScriptableObject
    {
        public new string name;
        public Sprite icon;
        protected float _duration;
        public float Duration => _duration;

        public int requiredActivationCredit;
        public List<PowerUpLevelData> powerUpLevelDatas;

        public virtual void SetLevel(int lvl)
        {
        }

        public virtual void OnEnd(TruckMovement truck)
        {
        }
        public virtual void OnStart(TruckMovement truck)
        {
        }
        public virtual void OnEveryFrame(TruckMovement truck)
        {
        }
    }
}
