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
        }

        public override int OnPlayerCollided()
        {
            PowerUpManager.instance.SelectPowerUp(powerUp);
            Destroy(gameObject);
            return -1;
        }
    }
}
