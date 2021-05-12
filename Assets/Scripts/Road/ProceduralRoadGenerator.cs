using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Road
{
    public class ProceduralRoadGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject Road;
        [SerializeField] private int existingRoadObjectCount;
        [SerializeField, Range(0, 100)] private float minRoadLength;
        [SerializeField, Range(0, 100)] private float maxRoadLength;
        [SerializeField] private float roadWidthHalf;
        [SerializeField] private float roadHeightHalf;
        [SerializeField] private int roadTransportAfter;

        private Vector3 pos;
        private List<Transform> roads;
        private int roadIndex = 0;

        public static ProceduralRoadGenerator instance;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            roads = new List<Transform>();
            pos.y = -roadHeightHalf;
            CreateRoads();
        }

        private void CreateRoads()
        {
            for (roadIndex = 0; roadIndex < existingRoadObjectCount; roadIndex++)
            {
                float roadLength = Random.Range(minRoadLength, maxRoadLength);
                float roadPos = roadLength / 2;
                GameObject road;
                if (roadIndex % 2 == 1) // goes right
                {
                    pos.x += roadPos;
                    road = Instantiate(Road, pos, Quaternion.identity);
                    road.transform.localScale = new Vector3(roadLength, roadHeightHalf * 2, roadWidthHalf * 2);
                    pos.x += roadPos - roadWidthHalf;
                    pos.z += roadWidthHalf;
                }
                else
                {
                    pos.z += roadPos;
                    road = Instantiate(Road, pos, Quaternion.identity);
                    road.transform.localScale = new Vector3(roadWidthHalf * 2, roadHeightHalf * 2, roadLength);
                    pos.z += roadPos - roadWidthHalf;
                    pos.x += roadWidthHalf;
                }
                roads.Add(road.transform);
            }
        }
        public void TruckRoadIndexSet(Transform road)
        {
            int currentRoad = roads.IndexOf(road);
            if(currentRoad >= roadTransportAfter)
            {
                TransportFirstRoadToEnd();
            }
        }
        private void TransportFirstRoadToEnd()
        {
            roadIndex++;
            float roadLength = Random.Range(minRoadLength, maxRoadLength);
            float roadPos = roadLength / 2;
            Transform road = roads[0];
            if (roadIndex % 2 == 0) // goes right, this changes here I dont know why :)
            {
                pos.x += roadPos;
                road.position = pos;
                road.transform.localScale = new Vector3(roadLength, roadHeightHalf * 2, roadWidthHalf*2);
                pos.x += roadPos - roadWidthHalf;
                pos.z += roadWidthHalf;
            }
            else
            {
                pos.z += roadPos;
                road.position = pos;
                road.transform.localScale = new Vector3(roadWidthHalf*2, roadHeightHalf * 2, roadLength);
                pos.z += roadPos - roadWidthHalf;
                pos.x += roadWidthHalf;
            }
            roads.RemoveAt(0);
            roads.Add(road);
        }
    }
}