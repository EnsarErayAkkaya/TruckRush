using System;
using UnityEngine;

namespace Project.Settings.Obstacles
{
    [Serializable]
    public class BarricadeSetting
    {
        public GameObject barricadePrefab;
        public float barricadeWidthHalf;
        public float barricadeLengthHalf;
        public float barricadeHeightHalf;
    }
}
