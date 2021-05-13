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
            if(collision.CompareTag(obstacleTag))
            {
                Obstacle o = collision.transform.GetComponent<Obstacle>();
                float damage = o.OnPlayerCollided();
                if (damage != -1)
                    playerHealth.Health -= damage;
            }
            else if (collision.CompareTag(coinTag))
            {
                Coin c = collision.transform.GetComponent<Coin>();
                int value = c.OnPlayerCollided();
                if (value != -1)
                {
                    CoinManager.instance.GainCoin(value);
                    Destroy(c.gameObject);
                }
            }
            else if (collision.CompareTag(gasStationTag))
            {
                GasStation g = collision.transform.GetComponent<GasStation>();
                int value = g.OnPlayerCollided();
                if (value != -1)
                {
                    playerFuel.GainFuel(value);
                }
            }
        }
    }
}