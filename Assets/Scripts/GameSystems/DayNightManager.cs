using UnityEngine;

namespace Project.GameSystems
{
    public class DayNightManager : MonoBehaviour
    {
        [SerializeField, Range(0.0f, 1.0f)] private float time;
        [SerializeField] private float fullDayLength;
        [SerializeField] private float startTime = 0.4f;

        [SerializeField] private Vector3 noon;
        private float timeRate;

        [Header("Sun")]
        [SerializeField] private Light sun;
        [SerializeField] private Gradient sunColor;
        [SerializeField] private AnimationCurve sunIntensity;
        
        [Header("Moon")]
        [SerializeField] private Light moon;
        [SerializeField] private Gradient moonColor;
        [SerializeField] private AnimationCurve moonIntensity;

        [Header("Other Lightning")]
        [SerializeField] private AnimationCurve lightningIntensityMultiplier;
        [SerializeField] private AnimationCurve reflectionsIntensityMultiplier;

        private bool flow;
        private int day;

        private void Start()
        {
            timeRate = 1.0f / fullDayLength;
            time = startTime;
            SetSunAndMoon();
            GameManager.instance.onGameStart += StartFlow;
        }

        private void Update()
        {
            if (flow)
            {
                //increment Time
                time += timeRate * Time.deltaTime;

                if (time >= 1.0f)
                {
                    time = 0.0f;
                    day++;
                }
                SetSunAndMoon();
                
            }

        }
        private void SetSunAndMoon()
        {
            //light Rotation
            sun.transform.eulerAngles = (time - 0.25f) * noon * 4.0f;
            moon.transform.eulerAngles = (time - 0.75f) * noon * 4.0f;

            // light intensity
            sun.intensity = sunIntensity.Evaluate(time);
            moon.intensity = moonIntensity.Evaluate(time);

            // change colors
            sun.color = sunColor.Evaluate(time);
            moon.color = moonColor.Evaluate(time);

            // enable / disable the sun
            if (sun.intensity == 0 && sun.gameObject.activeInHierarchy)
                sun.gameObject.SetActive(false);
            else if (sun.intensity > 0 && !sun.gameObject.activeInHierarchy)
                sun.gameObject.SetActive(true);

            // enable / disable the moon
            if (moon.intensity == 0 && moon.gameObject.activeInHierarchy)
                moon.gameObject.SetActive(false);
            else if (moon.intensity > 0 && !moon.gameObject.activeInHierarchy)
                moon.gameObject.SetActive(true);

            // lightning and reflections intensity
            RenderSettings.ambientIntensity = lightningIntensityMultiplier.Evaluate(time);
            RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(time);
        }
        private void StartFlow() => flow = true;

    }
}
