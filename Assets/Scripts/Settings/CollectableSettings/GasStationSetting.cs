using System;
using UnityEngine;

namespace Project.Settings.Collectables
{
    [Serializable]
    public class GasStationSetting
    {
        public GameObject gasStationPrefab;
        public int gasStationSpawnAfter;
        public int minGasStationFrequency;
        public int maxGasStationFrequency;
        public int minGasValue;
        public int maxGasValue;
        public int minRequiredCoinValue;
        public int maxRequiredCoinValue;
        public float gasStationLengthHalf;
        public float gasStationHeightHalf;
        public float gasStationWidthHalf;
    }
}
