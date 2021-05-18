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
        private List<Road> roads;
        private int roadIndex = 0;

        public int RoadIndex => roadIndex;
        public RoadSetting Setting => setting;

        public static ProceduralRoadGenerator instance;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            roads = new List<Road>();
            
            pos.y = -setting.roadHeightHalf;
            CreateRoads();
        }

        private void CreateRoads()
        {
            for (roadIndex = 0; roadIndex < setting.existingRoadObjectCount; roadIndex++)
            {
                float roadLength = Random.Range(setting.minRoadLength, setting.maxRoadLength);
                float roadPos = roadLength / 2;
                Vector3 raodStartingPos = pos;
                Road road;
                if (roadIndex % 2 == 1) // on X Axis
                {
                    pos.x += roadPos;
                    road = Instantiate(setting.Road, pos, Quaternion.identity).GetComponent<Road>();
                    road.Set(raodStartingPos, roadObjectsGenerator.totalLength, false);
                    road.transform.localScale = new Vector3(roadLength, setting.roadHeightHalf * 2, setting.roadWidthHalf * 2);
                    pos.x += roadPos - setting.roadWidthHalf;
                    pos.z += setting.roadWidthHalf;
                    roadObjectsGenerator.GenerateRoadObjects(road, false, (roadLength - setting.roadWidthHalf * 2));
                }
                else // on Z axis
                {
                    pos.z += roadPos;
                    road = Instantiate(setting.Road, pos, Quaternion.identity).GetComponent<Road>();
                    road.Set(raodStartingPos, roadObjectsGenerator.totalLength, true);
                    road.transform.localScale = new Vector3(setting.roadWidthHalf * 2, setting.roadHeightHalf * 2, roadLength);
                    pos.z += roadPos - setting.roadWidthHalf;
                    pos.x += setting.roadWidthHalf;
                    roadObjectsGenerator.GenerateRoadObjects(road, true, (roadLength - setting.roadWidthHalf * 2));
                }
                roads.Add(road);
            }
        }
        public void TruckRoadIndexSet(Road road)
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
            Road road = roads[0];
            road.Clear();
            float resRoadLength = (roadLength - setting.roadWidthHalf * 2);
            if (roadIndex % 2 == 0) // on X axis, this changes here I dont know why :)
            {
                road.Set(pos, roadObjectsGenerator.totalLength, false);
                pos.x += roadPos;
                road.transform.position = pos;
                road.transform.localScale = new Vector3(roadLength, setting.roadHeightHalf * 2, setting.roadWidthHalf *2);
                pos.x += roadPos - setting.roadWidthHalf;
                pos.z += setting.roadWidthHalf;
                roadObjectsGenerator.GenerateRoadObjects(road, false, resRoadLength);
            }
            else // on Z axis
            {
                road.Set(pos, roadObjectsGenerator.totalLength, true);
                pos.z += roadPos;
                road.transform.position = pos;
                road.transform.localScale = new Vector3(setting.roadWidthHalf *2, setting.roadHeightHalf * 2, roadLength);
                pos.z += roadPos - setting.roadWidthHalf;
                pos.x += setting.roadWidthHalf;
                roadObjectsGenerator.GenerateRoadObjects(road, true, resRoadLength);
            }
            roads.RemoveAt(0);
            roads.Add(road);
        }
    }
}