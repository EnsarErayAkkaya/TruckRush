using Project.GameSystems;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.UI
{
    public class EntranceUI : MonoBehaviour
    {
        [SerializeField] private GameObject[] swipeIndicators;

        [SerializeField] private TextMeshProUGUI highscoreText;
        [SerializeField] private TextMeshProUGUI headStartText;

        private void Start()
        {
            UpdateHeadStartText();
            highscoreText.text = DataManager.instance.savedData.highScore.ToString("#.#");
            if(DataManager.instance.savedData.isPlayedBefore == false)
            {
                foreach (GameObject item in swipeIndicators)
                {
                    item.SetActive(true);
                }
            }
        }
        public void UpdateHeadStartText()
        {
            headStartText.text = TruckManager.instance.HeadStartCount.ToString();
        }
        public void OnPlayButtonClicked()
        {
            SceneManager.LoadScene(1);
        }
    }
}