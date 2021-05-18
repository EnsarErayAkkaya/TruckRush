using Project.GameSystems;
using Project.UI.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private float maxHealth;
        [SerializeField] private TruckMovement truckMovement;
        [SerializeField] private PlayerUI playerUI;
        private bool canGetDamage = true;
        public float Health
        {
            get => health;
            set
            {
                if(canGetDamage)
                    health = value;
                playerUI.SetHealthFillImage(health / maxHealth);
                if (health <= 0)
                {
                    truckMovement.Stop();
                    GameManager.instance.PlayerLost();
                }
            }
        }
        private void Start()
        {
            maxHealth = health;
        }
        public void CantGetDamage() => canGetDamage = false;
        public void CanGetDamage() => canGetDamage = true;
    }
}