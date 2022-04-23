using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Settings
{
    [CreateAssetMenu(fileName = "new Market Object Setting", menuName = "Market/MarketObject")]
    public class MarketObjectSetting : ScriptableObject
    {
        public GameObject MarketObject;
        public string title;
        public int price;
    }
}