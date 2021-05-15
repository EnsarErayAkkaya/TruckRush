using UnityEngine;

namespace Project.GameSystems
{
    public class DayNightManager : MonoBehaviour
    {
        [SerializeField, Range(0.0f, 1.0f)] private float time;
        [SerializeField] private float fullDayLength;

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

        [SerializeField] private float openLightsThreshold; // if sun intensity lower than threshold open lights

        private bool flow;
        private int day;
        private bool isLightOpened;

        public delegate void OnDayNightStateChange();
        public OnDayNightStateChange onDay;
        public OnDayNightStateChange onNight;

        public static DayNightManager instance;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            timeRate = 1.0f / fullDayLength;
            time = Random.Range(.0f, 1.0f); // set start day time randomly
            SetSunAndMoon();
            GameManager.instance.onGameStart += StartFlow;
            GameManager.instance.onPlayerLost += StopFlow;
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

            if (sun.intensity < openLightsThreshold && !isLightOpened)
            {
                isLightOpened = true;
                onNight?.Invoke();
            }
            else if (sun.intensity >= openLightsThreshold && isLightOpened)
            {
                isLightOpened = false;
                onDay?.Invoke();
            }

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
        private void StopFlow() => flow = false;

    }
}
