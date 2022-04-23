using Project.GameSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class AdUI : MonoBehaviour
    {
        [SerializeField] private GameUI gameUI;
        [SerializeField] private Button rewardedVideoButton;
        [SerializeField] private Text rewardedVideoButtonText;

        private void Start()
        {
            rewardedVideoButton.onClick.AddListener(AdmobManager.instance.ShowRewardedAd);
            rewardedVideoButton.onClick.AddListener(OnClickRewardedVideoButton);
            GameManager.instance.onResurrect += OnResurrect;
        }

        private void OnClickRewardedVideoButton()
        {
            rewardedVideoButton.gameObject.SetActive(false);
            gameUI.UpdateCreditTexts();
        }
        private void OnResurrect()
        {
            rewardedVideoButton.gameObject.SetActive(true);
            rewardedVideoButtonText.text = "Double Your Coins";
        }
    }
}
