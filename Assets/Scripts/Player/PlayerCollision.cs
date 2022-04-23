using UnityEngine;
using Project.Obstacles;
using Project.Collectables;
using Project.GameSystems;
using Project.Interfaces;
using Project.Utility;

namespace Project.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private string obstacleTag;
        [SerializeField] private string coinTag;
        [SerializeField] private string gasStationTag;
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private PlayerFuel playerFuel;

        private CameraShake cameraShake;
        private void Start()
        {
            cameraShake = FindObjectOfType<CameraShake>();
        }
        private void OnTriggerEnter(Collider collision)
        {
            IFrangible frangible = collision.GetComponent<IFrangible>();

            if (frangible != null)
            {
                AudioManager.instance.Play("frangibleCrash");
                frangible.BreakOff();
            }

            if (collision.CompareTag(obstacleTag))
            {
                Obstacle o = collision.transform.GetComponent<Obstacle>();
                if (o != null)
                {
                    float damage = o.OnPlayerCollided();
                    if (damage != -1)
                        playerHealth.Health -= damage;
                }
                else
                {
                    o = collision.transform.parent.GetComponent<Obstacle>();
                    float damage = o.OnPlayerCollided();
                    if (damage != -1)
                        playerHealth.Health -= damage;
                }
                cameraShake.Shake(1f, 5f);
            }
            else 
            {
                Collectable c = collision.transform.GetComponent<Collectable>();
                if(c != null)
                {
                    int value = c.OnPlayerCollided();
                    if (collision.CompareTag(coinTag))
                    {
                        if (value != -1)
                        {
                            CoinManager.instance.GainCoin(value);
                            Destroy(c.gameObject);
                        }
                    }
                    else if (collision.CompareTag(gasStationTag))
                    {
                        if (value != -1)
                        {
                            playerFuel.GainFuel(value);
                        }
                    }
                }
            }
        }
    }
}