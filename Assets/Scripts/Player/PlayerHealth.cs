using Project.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private TruckMovement truckMovement;
        private bool canGetDamage = true;
        public float Health
        {
            get => health;
            set
            {
                if(canGetDamage)
                    health = value;
                if (health <= 0)
                {
                    truckMovement.Stop();
                    GameManager.instance.PlayerLost();
                }
            }
        }
        public void CantGetDamage() => canGetDamage = false;
        public void CanGetDamage() => canGetDamage = true;
    }
}