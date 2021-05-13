using UnityEngine;
using TMPro;
using Project.GameSystems;

namespace Project.UI
{
    public class GameUI : MonoBehaviour
    {
        public TextMeshProUGUI coinText;

        private void Start()
        {
            CoinManager.instance.onCoinChange += SetCoinText;
        }

        public void SetCoinText(int c)
        {
            coinText.text = c.ToString();
        }
    }
}