using UnityEngine;

namespace Project.Settings.Obstacles
{
    [System.Serializable]
    public class CrossroadSetting
    {
        public GameObject crossroadPrefab;
        public float crossroadLengthHalf;
        public float crossroadHeight;
        public int crossroadSpawnAfter;
        public int crossroadSpawnFreaquency;
        public int maxCrossroadOnRoad;
        public int minDamage;
        public int maxDamage;
    }
}
