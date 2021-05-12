using UnityEngine;

namespace Project.Road
{
    public class RoadObjectsGenerator : MonoBehaviour
    {
        [SerializeField] private RoadSetting setting;

        private float roadWidthHalf;
        private float roadHeightHalf;
        private void Start()
        {
            roadWidthHalf = ProceduralRoadGenerator.instance.RoadHalfWidth;
            roadHeightHalf = ProceduralRoadGenerator.instance.RoadHalfHeight;
        }

        private void GenerateRoadObjects(Transform road)
        {
            int barricadeCount = Random.Range(0, setting.maxBarricadeCount);

            for (int i = 0; i < barricadeCount; i++)
            {
            }
        }
    }
}
