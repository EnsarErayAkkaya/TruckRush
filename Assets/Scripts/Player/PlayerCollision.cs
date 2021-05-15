using UnityEngine;
using Project.Obstacles;
using Project.Collectables;
using Project.GameSystems;

namespace Project.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private string obstacleTag;
        [SerializeField] private string coinTag;
        [SerializeField] private string gasStationTag;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private PlayerFuel playerFuel;
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag(obstacleTag))
            {
                Obstacle o = collision.transform.GetComponent<Obstacle>();
                float damage = o.OnPlayerCollided();
                if (damage != -1)
                    playerHealth.Health -= damage;
            }
            else 
            {
                Collectable c = collision.transform.GetComponent<Collectable>();
                if(c != null)
                {
                    if (collision.CompareTag(coinTag))
                    {
                        int value = c.OnPlayerCollided();
                        if (value != -1)
                        {
                            CoinManager.instance.GainCoin(value);
                            Destroy(c.gameObject);
                        }
                    }
                    else if (collision.CompareTag(gasStationTag))
                    {
                        int value = c.OnPlayerCollided();
                        if (value != -1)
                        {
                            playerFuel.GainFuel(value);
                        }
                    }
                    else
                    {
                        c.OnPlayerCollided();
                    }
                }
            }
        }
    }
}