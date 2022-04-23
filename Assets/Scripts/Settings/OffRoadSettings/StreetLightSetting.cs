using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Settings
{
    [System.Serializable]
    public class StreetLightSetting
    {
        public GameObject streetLightPrefab;
        public int streetLightSpawnFreaquency;
        public float maxStartingSpace;
        public float streetLightHeight;
        public float streetLightWidth;
    }
}