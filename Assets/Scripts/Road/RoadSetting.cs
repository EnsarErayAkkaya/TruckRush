using Project.Obstacles.Settings;
using UnityEngine;

namespace Project.Road
{
    public class RoadSetting : ScriptableObject
    {
        [Header("Spawn Counts")]
        [Range(0, 5)] public  int maxBarricadeCount;
        [Range(0, 4)] public int maxCoinCountX5;

        [Header("Barricade Setting")]
        public BarricadeSetting barricadeSetting;

    }
}