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
        private string powerUpName;
        public void Start()
        {
            powerUpName = PowerUpManager.instance.GetRandomPowerUp();
        }

        public override int OnPlayerCollided()
        {
            PowerUpManager.instance.UsePowerUp(powerUpName);
            Destroy(gameObject);
            return -1;
        }
    }
}
