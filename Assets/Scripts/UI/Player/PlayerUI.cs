using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Image fuelImage;

        /// <summary>
        /// Set fuel fill amount. fuel value is 0-1
        /// </summary>
        /// <param name="fuel"></param>
        public void SetFuelFillImage(float fuel)
        {
            fuelImage.fillAmount = fuel;
        }
    }
}
