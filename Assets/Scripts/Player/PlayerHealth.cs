using Project.GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float health;
        public float Health
        {
            get => health;
            set
            {
                health = value;
                if (health <= 0)
                    GameManager.instance.OnPlayerFailure();

            }
        }
    }
}