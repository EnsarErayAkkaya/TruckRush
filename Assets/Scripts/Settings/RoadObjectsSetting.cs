using Project.Settings.Obstacles;
using Project.Settings.Collectables;
using UnityEngine;

namespace Project.Settings
{
    [CreateAssetMenu(fileName = "new RoadObjectsSetting", menuName = "Road/RoadObjectsSetting")]
    public class RoadObjectsSetting : ScriptableObject
    {
        public RoadLengthMinMax[] roadObjectCounts;
        [System.Serializable]
        public struct RoadLengthMinMax
        {
            public float length;
            public int min;
            public int max;
        }
        [Header("Barricade Setting")]
        public BarricadeSetting barricadeSetting;
        [Header("Big Barricade Setting")]
        public BigBarricadeSetting bigBarricadeSetting;
        [Header("Coin Setting")]
        public CoinSetting coinSetting;
        [Header("Gas Station Setting")]
        public GasStationSetting gasStationSetting;
        [Header("Power Up Setting")]
        public PowerUpSetting powerUpSetting;
        [Header("Distance Milestone Setting")]
        public DistanceMilestoneSetting distanceMilestoneSetting;
        [Header("Empty Space Setting")]
        public float minEmptySpaceLength;
        public float maxEmptySpaceLength;
    }
}