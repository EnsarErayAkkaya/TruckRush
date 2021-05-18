using UnityEngine;
using Project.GameSystems;
using UnityEngine.UI;
using TMPro;

namespace Project.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Text coinText;
        [SerializeField] private GameObject playButton;
        [SerializeField] private GameObject failiurePanel;
        [SerializeField] private TextMeshProUGUI distance;
        [SerializeField] private TextMeshProUGUI usedPowerUp;
        [SerializeField] private TextMeshProUGUI passedMilestone;
        [SerializeField] private TextMeshProUGUI gainedCredit;
        [SerializeField] private TextMeshProUGUI totalCredit;
        

        private void Start()
        {
            CoinManager.instance.onCoinChange += SetCoinText;
            GameManager.instance.onPlayerLost += ActivateFailurePanel;
        }

        private void SetCoinText(int c)
        {
            coinText.text = c.ToString();
        }
        public void OnPlayButtonClicked()
        {
            GameManager.instance.StartGame();
            playButton.SetActive(false);
        }
        public void OnRestartButtonClicked()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        public void OnQuitButtonClicked()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        private void ActivateFailurePanel()
        {
            failiurePanel.SetActive(true);
            float distanceTravelled = ScoreManager.instance.DistanceTravelled;
            float powerUpCount = ScoreManager.instance.UsedPowerUpCount;
            float achievedMilestoneCount = ScoreManager.instance.AchievedMilestoneCount;
            distance.text = distanceTravelled.ToString("#.#") + " Meter";
            usedPowerUp.text = powerUpCount + " X " + CreditManager.instance.PowerUpCreditMultiplier;
            passedMilestone.text = achievedMilestoneCount + " X " + CreditManager.instance.MilestoneCreditMultiplier;
            gainedCredit.text = ScoreManager.instance.GainedCredit.ToString();
            totalCredit.text = CreditManager.instance.Credit.ToString();
        }
    }
}