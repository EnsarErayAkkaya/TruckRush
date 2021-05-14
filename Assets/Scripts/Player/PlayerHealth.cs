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
        public float Health
        {
            get => health;
            set
            {
                health = value;
                if (health <= 0)
                {
                    truckMovement.Stop();
                    GameManager.instance.PlayerLost();
                }
            }
        }
    }
}