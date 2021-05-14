using UnityEngine;
using Project.GameSystems;
using UnityEngine.UI;

namespace Project.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Text coinText;
        [SerializeField] private GameObject playButton;
        [SerializeField] private GameObject restartButton;
        

        private void Start()
        {
            CoinManager.instance.onCoinChange += SetCoinText;
            GameManager.instance.onPlayerLost += ActivateRestartButton;
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
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        private void ActivateRestartButton()
        {
            restartButton.SetActive(true);
        }
    }
}