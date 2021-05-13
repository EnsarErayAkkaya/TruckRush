using UnityEngine;
using TMPro;
namespace Project.UI.Collectables
{
    public class GasStationUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI requiredCoinText;
        [SerializeField] private TextMeshProUGUI gasValueText;
        public void SetGasStationTexts(int requiredCoin, int gasValue)
        {
            requiredCoinText.text = requiredCoin.ToString();
            gasValueText.text = gasValue.ToString();
        }
    }
}