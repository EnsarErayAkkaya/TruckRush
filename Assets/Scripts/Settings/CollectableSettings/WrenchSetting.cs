using System;
using UnityEngine;

namespace Project.Settings.Collectables
{
    [Serializable]
    public class WrenchSetting
    {
        public GameObject WrenchPrefab;
        public float wrenchLengthHalf;
        public float wrenchHeightHalf;
        public int minWrenchFrequency;
        public int wrenchWillStartFrom;
    }
}
