using System;
using System.Collections;
using UnityEngine;

namespace Project.Utility
{
    public class CanvasGroupUtility
    {
        public static IEnumerator ChangeAlpha(CanvasGroup cg, float start, float end, float lerpTime = 0.5f)
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
    }
}
