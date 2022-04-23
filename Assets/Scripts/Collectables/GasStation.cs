using Project.GameSystems;
using Project.UI.Collectables;
using UnityEngine;

namespace Project.Collectables
{
    [RequireComponent(typeof(GasStationUI))]
    public class GasStation : Collectable
    {
        private int requiredCoin;
        public void Set(int requiredCoin, int gasValue)
        {
            this.requiredCoin = requiredCoin;
            this.value = gasValue;
            GetComponent<GasStationUI>().SetGasStationTexts(requiredCoin, value);
        }

        public override int OnPlayerCollided()
        {
            if (CoinManager.instance.IsCoinSufficient(requiredCoin) && !destroyed)
            {
                PlayCollectSound();
                destroyed = true;
                CoinManager.instance.LoseCoin(requiredCoin);
                return value;
            }
            return -1;
        }
    }
}