using Project.Settings;
using Project.Spawners;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Road
{
    public class RoadObjectsGenerator : MonoBehaviour
    {
        [SerializeField] private RoadObjectsSetting setting;

        private BarricadeSpawner barricadeSpawner;
        private CoinSpawner coinSpawner;

        private float roadWidthHalf;
        private float roadHeightHalf;

        private float startingPoint;
        private float endingPoint;

        private RoadSegmentation roadSegmentation;
        private Vector3 spawnPos;
        private bool onZAxis;
        private void Start()
        {
            roadSegmentation = new RoadSegmentation();
            barricadeSpawner = new BarricadeSpawner(setting.barricadeSetting);
            coinSpawner = new CoinSpawner(setting.coinSetting);

        }

        public void GenerateRoadObjects(Transform road, bool onZAxis, float roadLength)
        {
            this.onZAxis = onZAxis;
            if (onZAxis)
            {
                startingPoint = road.position.z - roadLength / 2;
                endingPoint = road.position.z + roadLength / 2;
                spawnPos = new Vector3(road.position.x, 0, startingPoint);
            }
            else
            {
                startingPoint = road.position.x - roadLength / 2;
                endingPoint = road.position.x + roadLength / 2;
                spawnPos = new Vector3(startingPoint, 0, road.position.z);
            }

            roadSegmentation.Set(startingPoint, endingPoint);

            int objectCount = ObjectCountAccordingToRoadLength(roadLength);
            Debug.Log("Road Object Count: " + objectCount + " for road " + road.name);

            for (int i = 0; i < objectCount; i++)
            {
                int code = Random.Range(0, 2);
                switch (code)
                {
                    case 0:
                        GenerateBarricade();
                        break;
                    case 1:
                        GenerateEmptySpace();
                        break;
                    default:
                        break;
                }
            }
        }

        private void GenerateBarricade()
        {
            Debug.Log("Generating Barricade");
            RoadBlock block = null;
            if(onZAxis)
            {
                if (roadSegmentation.AllocateSpace(setting.barricadeSetting.barricadeLengthHalf * 2, ref block))
                {
                    Debug.Log("Space Allocated, block: " + block.ToString());
                    barricadeSpawner.Spawn(spawnPos, onZAxis);
                }
            }
            else
            {
                if (roadSegmentation.AllocateSpace(setting.barricadeSetting.barricadeLengthHalf * 2, ref block))
                {
                    Debug.Log("Space Allocated");
                    barricadeSpawner.Spawn(spawnPos, onZAxis);
                }
            }
        }
        private void GenerateEmptySpace()
        {
            Debug.Log("Generating EmptySpace");
            RoadBlock block = null;
            float length = Random.Range(setting.minEmptySpaceLength, setting.maxEmptySpaceLength);
            roadSegmentation.AllocateSpace(length, ref block);
        }
        private int ObjectCountAccordingToRoadLength(float length)
        {
            foreach (var item in setting.roadObjectCounts)
            {
                if (item.length < length)
                    return Random.Range(item.min, item.max);
            }
            return 1;
        }
    }
}
