using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Road
{
    public class RoadObjectsGenerator : MonoBehaviour
    {
        [SerializeField, Range(0, 100)] private int maxBarricadeCount;
        [SerializeField, Range(0, 4)] private int coinCountX5;


        private float roadWidthHalf;
        private float roadHeightHalf;
        private void Start()
        {
            roadWidthHalf = ProceduralRoadGenerator.instance.RoadHalfWidth;
            roadHeightHalf = ProceduralRoadGenerator.instance.RoadHalfHeight;
        }


    }
}
