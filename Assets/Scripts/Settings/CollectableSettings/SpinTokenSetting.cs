using System;
using UnityEngine;

namespace Project.Settings.Collectables
{
    [Serializable]
    public class SpinTokenSetting
    {
        public GameObject spinTokenPrefab;
        public float spinTokenLengthHalf;
        public float spinTokenHeightHalf; 
        public int minTokenFrequency;
        public int tokenWillStartFrom;
    }
}
