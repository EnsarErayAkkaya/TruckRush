using UnityEngine;

namespace Project.GameSystems
{
    public class DayNightManager : MonoBehaviour
    {
        [SerializeField, Range(0.0f, 1.0f)] private float time;
        [SerializeField] private float fullDayLength;
        [SerializeField] private float startTime = 0.4f;
        private float timeRate;
        [SerializeField] private Vector3 noon;

        [Header("Sun")]
        [SerializeField] private Light sun;
        [SerializeField] private Gradient sunColor;
        [SerializeField] private AnimationCurve sunIntensity;
        
        [Header("Moon")]
        [SerializeField] private Light moon;
        [SerializeField] private Gradient moonColor;
        [SerializeField] private AnimationCurve moonIntensity;


    }
}
