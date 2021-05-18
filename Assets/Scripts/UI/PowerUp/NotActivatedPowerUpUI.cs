using Project.GameSystems;
using Project.PowerUps;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class NotActivatedPowerUpUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI requiredCredit;

        private PowerUp powerUp;

        public void Set(PowerUp p)
        {
            icon.sprite = p.icon;
            requiredCredit.text = p.requiredActivationCredit.ToString();
            this.powerUp = p;
        }

        public void ActivatePowerUp()
        {
            if (CreditManager.instance.IsCreditSufficient(powerUp.requiredActivationCredit))
            {
                CreditManager.instance.LoseCredit(powerUp.requiredActivationCredit);
                PowerUpManager.instance.ActivatePowerUp(powerUp);
            }
        }
    }
}