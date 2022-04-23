using Project.GameSystems;
using System;

namespace Project.Obstacles
{
    public class Pedestrian : Obstacle
    {
        public void Set(int min, int max)
        {
            damage = new Random().Next(min, max);
        }
        public override float OnPlayerCollided()
        {
            if (!destroyed)
            {
                destroyed = true;
                PlayCrashSound();
                CoinManager.instance.LoseCoin((int)damage);
                UI.Notification.instance.AddNotification(damage + " Coin fined!");
                Destroy(gameObject);
                return -1;
            }
            return -1;
        }
    }
}