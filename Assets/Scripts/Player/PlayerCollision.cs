using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Obstacles;

namespace Project.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private string obstacleTag;
        [SerializeField] private PlayerHealth playerHealth;
        private void OnTriggerEnter(Collider collision)
        {
            if(collision.CompareTag(obstacleTag))
            {
                Obstacle o = collision.transform.GetComponent<Obstacle>();
                float damage = o.OnPlayerCollided();
                if (damage != -1)
                    playerHealth.Health -= damage;
            }
        }
    }
}