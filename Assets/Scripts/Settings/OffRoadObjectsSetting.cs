using Project.Settings.Obstacles;
using Project.Settings.Collectables;
using UnityEngine;
using System.Collections.Generic;

namespace Project.Settings
{
    [CreateAssetMenu(fileName = "OffRoadObjectsSetting", menuName = "Road/OffRoadObjectsSetting")]
    public class OffRoadObjectsSetting : ScriptableObject
    {
        [Header("Gas Station Setting")]
        public GasStationSetting gasStationSetting;

        #region Biom Classes
        [System.Serializable]
        public struct Biom
        {
            public BiomType biomType;
            public GameObject[] biomObjects;
        }
        [System.Serializable]
        public enum BiomType
        {
            Village, Valley
        }
        #endregion
        [Header("Bioms")]
        public List<Biom> bioms;


        [Header("Off Road Object Setting")]
        public float offRoadObjectLength;
        public float offRoadObjectWidth;

        [Header("Street Light Setting")]
        public StreetLightSetting streetLightSetting;
    }
    
}