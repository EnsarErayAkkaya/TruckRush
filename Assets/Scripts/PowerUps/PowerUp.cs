using Project.Player;
using Project.UI;
using System.Collections;
using UnityEngine;

namespace Project.PowerUps
{
    public abstract class PowerUp : ScriptableObject
    {
        public new string name;
        public Sprite icon;
        public float duration;

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
