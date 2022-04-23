using UnityEngine;

namespace Project.Settings
{
    [System.Serializable]
    public class BigBarricadeSetting
    {
        public GameObject bigBarricadePrefab;
        public int bigBarricadeSpawnAfter;
        public int bigBarricadeSpawnFreaquency;
        public float bigBarricadeLengthHalf;
        public float bigBarricadeHeightHalf;
        public float bigBarricadeWidthHalf;
    }
}
