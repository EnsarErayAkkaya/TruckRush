using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Slider fuelSlider;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float healhBarShowDuration;

        /// <summary>
        /// Set fuel fill amount. fuel value is 0-1
        /// </summary>
        /// <param name="fuel"></param>
        public void SetFuelFillImage(float fuel)
        {
            fuelSlider.value = fuel;
        }
        /// <summary>
        /// Set health fill amount. health value is 0-1
        /// </summary>
        /// <param name="health"></param>
        public void SetHealthFillImage(float health)
        {
            StartCoroutine(ShowAndHideHealthBar(health));
            
        }
        private IEnumerator ChangeAlpha(CanvasGroup cg, float start, float end, float lerpTime = 0.5f )
        {
            float timeStartedLerping = Time.time;
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / lerpTime;
            while (true)
            {
                timeSinceStarted = Time.time - timeStartedLerping;
                percentageComplete = timeSinceStarted / lerpTime;

                float currentValue = Mathf.Lerp(start, end, percentageComplete);

                cg.alpha = currentValue;
                if (percentageComplete >= 1) break;

                yield return new WaitForEndOfFrame();
            }
        }
        private IEnumerator ShowAndHideHealthBar(float health)
        {
            yield return ChangeAlpha(canvasGroup, canvasGroup.alpha, 1);
            healthSlider.value = health;
            yield return new WaitForSeconds(healhBarShowDuration);
            yield return ChangeAlpha(canvasGroup, canvasGroup.alpha, 0);
        }
    }
}
