using UnityEngine;
using Project.GameSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Project.Utility;

namespace Project.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Text coinText;
        [SerializeField] private Text wrenchText;
        [SerializeField] private GameObject playButton;
        [SerializeField] private GameObject resumeButton;
        [SerializeField] private GameObject failiurePanel;
        [SerializeField] private GameObject resurrectionPanel;
        [SerializeField] private CanvasGroup swipeTutorialCanvasGroup;
        [SerializeField] private TextMeshProUGUI distance;
        [SerializeField] private TextMeshProUGUI usedPowerUp;
        [SerializeField] private TextMeshProUGUI passedMilestone;
        [SerializeField] private TextMeshProUGUI gainedCredit;
        [SerializeField] private TextMeshProUGUI totalCredit;
        [SerializeField] private Slider fuelSlider;

        [SerializeField] private float coinColorChangeDuration;

        [SerializeField] private Transform steeringWheelParent;
        [SerializeField] private Image steeringWheelmage;
        [SerializeField] private Image truckLayoutImage;
        [SerializeField] private Vector2 wrenchNoTruckLayoutPos;

        private float steeringWheelRotation;
        private void Awake()
        {
            if (DataManager.instance.savedData.isPlayedBefore == false)
            {
                swipeTutorialCanvasGroup.gameObject.SetActive(true);
                StartCoroutine(CanvasGroupUtility.ChangeAlpha(swipeTutorialCanvasGroup, 1, 0, 8));
            }
        }


        private void Start()
        {
            CoinManager.instance.onCoinChange += SetCoinText;
            GameManager.instance.onResurrect += ActivateResurrectionPanel;
            GameManager.instance.onPlayerLost += ActivateFailurePanel;
        }

        private void SetCoinText(int c)
        {
            coinText.text = c.ToString();
            StartCoroutine(ChangeCoinText());
        }
        private IEnumerator ChangeCoinText()
        {
            float t = 0;
            while (t < coinColorChangeDuration)
            {
                t += Time.deltaTime * 1.5f;
                coinText.color = Color.Lerp(coinText.color, Color.green, t);
                yield return new WaitForEndOfFrame();
            }
            t = 0;
            while (t < coinColorChangeDuration)
            {
                t += Time.deltaTime * 1.5f;
                coinText.color = Color.Lerp(coinText.color, Color.white, t);
                yield return new WaitForEndOfFrame();
            }
        }

        public void SetWrenchText(int c)
        {
            wrenchText.text = c.ToString() + " / 3";
        }
        public void OnPlayButtonClicked()
        {
            GameManager.instance.StartGame();
            playButton.SetActive(false);
            ActivateSteeringWheel();
        }
        public void OnRestartButtonClicked()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        public void OnResumeButtonClicked()
        {
            GameManager.instance.StartGame();
            resumeButton.SetActive(false);
            ActivateSteeringWheel();
        }
        public void OnQuitButtonClicked()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        /// <summary>
        /// Activate failure panel and fill texts
        /// </summary>
        public void ActivateFailurePanel()
        {
            DeactivateSteeringWheel();
            failiurePanel.SetActive(true);
            float distanceTravelled = ScoreManager.instance.DistanceTravelled;
            float powerUpCount = ScoreManager.instance.UsedPowerUpCount;
            float achievedMilestoneCount = ScoreManager.instance.AchievedMilestoneCount;
            distance.text = ((int)distanceTravelled).ToString() + " Meters";
            usedPowerUp.text = powerUpCount + " X " + CreditManager.instance.PowerUpCreditMultiplier;
            passedMilestone.text = achievedMilestoneCount + " X " + CreditManager.instance.MilestoneCreditMultiplier;
            gainedCredit.text = ScoreManager.instance.GainedCredit.ToString();
            totalCredit.text = CreditManager.instance.Credit.ToString();
        }

        private void ActivateResurrectionPanel()
        {
            failiurePanel.SetActive(false);
            resurrectionPanel.SetActive(true);
        }
        public void UpdateCreditTexts()
        {
            gainedCredit.text = ScoreManager.instance.GainedCredit.ToString() + " X 2";
            totalCredit.text = (CreditManager.instance.Credit + ScoreManager.instance.GainedCredit).ToString();
        }
        /// <summary>
        /// Set fuel fill amount. fuel value is 0-1
        /// </summary>
        /// <param name="fuel"></param>
        public void SetFuelFillImage(float fuel)
        {
            fuelSlider.value = fuel;
        }

        public void SetTruckLayoutAndSteeringWheel(Sprite wheel, Sprite layout)
        {
            if (DataManager.instance.savedData.isTruckLayoutHidden == false)
            {
                steeringWheelmage.sprite = wheel;
                truckLayoutImage.sprite = layout;
            }
            else
            {
                // hide UI
                steeringWheelmage.gameObject.SetActive(false);
                truckLayoutImage.gameObject.SetActive(false);

                // Set Wrench Parent Pos
                RectTransform wParent = wrenchText.transform.parent.GetComponentInParent<RectTransform>();
                wParent.anchorMin = new Vector2(0, 1);
                wParent.anchorMax = new Vector2(0, 1);
                wParent.pivot = new Vector2(0, 1);
                wParent.anchoredPosition = wrenchNoTruckLayoutPos;
            }
        }

        public void RotateSteeringWheel(float rotateAmount)
        {
            steeringWheelRotation = rotateAmount;
        }
        private void ActivateSteeringWheel()
        {
            StartCoroutine(RotateWheelEnumerator());
        }
        private void DeactivateSteeringWheel()
        {
            StopCoroutine(RotateWheelEnumerator());
        }

        private IEnumerator RotateWheelEnumerator()
        {
            while (true)
            {
                steeringWheelParent.rotation = Quaternion.Lerp(
                    steeringWheelParent.rotation,
                    Quaternion.Euler(0, 0, Mathf.Acos(steeringWheelRotation) * Mathf.Rad2Deg - 90),
                    Time.deltaTime
                );
                yield return null;
            }
        }
    }
}