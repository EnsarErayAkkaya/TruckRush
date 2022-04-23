using Project.GameSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Collectables
{
    public class PowerUp : Collectable
    {
        private PowerUps.PowerUp powerUp;
        public void Start()
        {
            powerUp = PowerUpManager.instance.GetRandomPowerUp();
            if (powerUp == null)
                Destroy(gameObject);
        }

        public override int OnPlayerCollided()
        {
            PlayCollectSound();
            PowerUpManager.instance.SelectPowerUp(powerUp);
            Destroy(gameObject);
            return -1;
        }
    }
}
