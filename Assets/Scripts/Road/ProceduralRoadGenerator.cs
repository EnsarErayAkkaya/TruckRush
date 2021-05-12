using Project.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Road
{
    public class ProceduralRoadGenerator : MonoBehaviour
    {
        [SerializeField] private RoadSetting setting;
        [SerializeField] private RoadObjectsGenerator roadObjectsGenerator;
        private Vector3 pos;
        private List<Transform> roads;
        private int roadIndex = 0;

        public RoadSetting Setting => setting;

        public static ProceduralRoadGenerator instance;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            roads = new List<Transform>();
            
            pos.y = -setting.roadHeightHalf;
            CreateRoads();
        }

        private void CreateRoads()
        {
            for (roadIndex = 0; roadIndex < setting.existingRoadObjectCount; roadIndex++)
            {
                float roadLength = Random.Range(setting.minRoadLength, setting.maxRoadLength);
                float roadPos = roadLength / 2;
                GameObject road;
                if (roadIndex % 2 == 1) // on X Axis
                {
                    pos.x += roadPos;
                    road = Instantiate(setting.Road, pos, Quaternion.identity);
                    road.transform.localScale = new Vector3(roadLength, setting.roadHeightHalf * 2, setting.roadWidthHalf * 2);
                    pos.x += roadPos - setting.roadWidthHalf;
                    pos.z += setting.roadWidthHalf;
                    roadObjectsGenerator.GenerateRoadObjects(road.transform, false, (roadLength - setting.roadWidthHalf * 2));
                }
                else // on Z axis
                {
                    pos.z += roadPos;
                    road = Instantiate(setting.Road, pos, Quaternion.identity);
                    road.transform.localScale = new Vector3(setting.roadWidthHalf * 2, setting.roadHeightHalf * 2, roadLength);
                    pos.z += roadPos - setting.roadWidthHalf;
                    pos.x += setting.roadWidthHalf;
                    roadObjectsGenerator.GenerateRoadObjects(road.transform, true, (roadLength - setting.roadWidthHalf * 2));
                }
                roads.Add(road.transform);
            }
        }
        public void TruckRoadIndexSet(Transform road)
        {
            int currentRoad = roads.IndexOf(road);
            if(currentRoad >= setting.roadTransportAfter)
            {
                TransportFirstRoadToEnd();
            }
        }
        private void TransportFirstRoadToEnd()
        {
            roadIndex++;
            float roadLength = Random.Range(setting.minRoadLength, setting.maxRoadLength);
            float roadPos = roadLength / 2;
            Transform road = roads[0];
            if (roadIndex % 2 == 0) // on X axis, this changes here I dont know why :)
            {
                pos.x += roadPos;
                road.position = pos;
                road.transform.localScale = new Vector3(roadLength, setting.roadHeightHalf * 2, setting.roadWidthHalf *2);
                pos.x += roadPos - setting.roadWidthHalf;
                pos.z += setting.roadWidthHalf;
            }
            else // on Z axis
            {
                pos.z += roadPos;
                road.position = pos;
                road.transform.localScale = new Vector3(setting.roadWidthHalf *2, setting.roadHeightHalf * 2, roadLength);
                pos.z += roadPos - setting.roadWidthHalf;
                pos.x += setting.roadWidthHalf;
            }
            roads.RemoveAt(0);
            roads.Add(road);
        }
    }
}