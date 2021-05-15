using Project.Player;
using System.Collections;
using UnityEngine;

namespace Project.PowerUps
{
    public abstract class PowerUp : ScriptableObject
    {
        public new string name;
        public Sprite icon;

        public abstract IEnumerator Use(TruckMovement truck);
    }
}
