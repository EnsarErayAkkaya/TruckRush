using Project.Settings.Obstacles;
using Project.Settings.Collectables;
using UnityEngine;

namespace Project.Settings
{
    [CreateAssetMenu(fileName = "new RoadSetting",menuName ="Road/RoadSetting")]
    public class RoadSetting : ScriptableObject
    {
        public GameObject Road;
        public int existingRoadObjectCount;
        [Range(0, 100)] public float minRoadLength;
        [Range(0, 100)] public float maxRoadLength;
        public float roadWidthHalf;
        public float roadHeightHalf;
        public int roadTransportAfter;

    }
}