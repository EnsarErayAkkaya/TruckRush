using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Project.Utility;

namespace Project.UI.Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float healhBarShowDuration;
        [SerializeField] private Text trackBackWriting;

        /// <summary>
        /// Set health fill amount. health value is 0-1
        /// </summary>
        /// <param name="health"></param>
        public void SetHealthFillImage(float health)
        {
            StartCoroutine(ShowAndHideHealthBar(health));
            
        }
        private IEnumerator ShowAndHideHealthBar(float health)
        {
            yield return CanvasGroupUtility.ChangeAlpha(canvasGroup, canvasGroup.alpha, 1);
            healthSlider.value = health;
            yield return new WaitForSeconds(healhBarShowDuration);
            yield return CanvasGroupUtility.ChangeAlpha(canvasGroup, canvasGroup.alpha, 0);
        }

        public void SetTruckBackWriting(string t)
        {
            trackBackWriting.text = t;
        }
    }
}
