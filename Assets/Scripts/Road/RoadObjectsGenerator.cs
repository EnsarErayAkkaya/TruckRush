using Project.Settings;
using Project.Spawners;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Road
{
    public class RoadObjectsGenerator : MonoBehaviour
    {
        [SerializeField] private RoadObjectsSetting setting;

        #region Spawners
        private BarricadeSpawner barricadeSpawner;
        private CoinSpawner coinSpawner;
        private GasStationSpawner gasStationSpawner;
        private PowerUpSpawner powerUpSpawner;
        #endregion

        private float startingPoint;
        private float endingPoint;

        private RoadSegmentation roadSegmentation;
        private Vector3 spawnPos;
        private bool onZAxis;
        private int lastGasStationCreatedIndex;
        private int lastPowerUpCreatedIndex;
        private void Awake()
        {
            roadSegmentation = new RoadSegmentation();

            barricadeSpawner = new BarricadeSpawner(setting.barricadeSetting);
            coinSpawner = new CoinSpawner(setting.coinSetting);
            gasStationSpawner = new GasStationSpawner(setting.gasStationSetting);
            powerUpSpawner = new PowerUpSpawner(setting.powerUpSetting);
        }

        public void GenerateRoadObjects(Road road, bool onZAxis, float roadLength)
        {
            this.onZAxis = onZAxis;
            if (onZAxis)
            {
                startingPoint = road.transform.position.z - roadLength / 2;
                endingPoint = road.transform.position.z + roadLength / 2;
                spawnPos = new Vector3(road.transform.position.x, 0, startingPoint);
            }
            else
            {
                startingPoint = road.transform.position.x - roadLength / 2;
                endingPoint = road.transform.position.x + roadLength / 2;
                spawnPos = new Vector3(startingPoint, 0, road.transform.position.z);
            }

            roadSegmentation.Set(startingPoint, endingPoint);

            int objectCount = ObjectCountAccordingToRoadLength(roadLength);

            for (int i = 0; i < objectCount; i++)
            {
                int code = Random.Range(0, 3);
                GameObject g;
                switch (code)
                {
                    case 0:
                         g = GenerateBarricade();
                        if (g != null)
                            road.AddObject(g);
                        break;
                    case 1:
                        GenerateEmptySpace();
                        break;
                    case 2:
                        g = GenerateCollectable();
                        if (g != null)
                            road.AddObject(g);
                        break;
                    default:
                        break;
                }
            }
        }

        private GameObject GenerateBarricade()
        {
            if(onZAxis)
            {
                float value = roadSegmentation.AllocateSpace(setting.barricadeSetting.barricadeLengthHalf * 2);
                if (value != -1)
                {
                    spawnPos.z = value;
                    return barricadeSpawner.Spawn(spawnPos, onZAxis);
                }
            }
            else
            {
                float value = roadSegmentation.AllocateSpace(setting.barricadeSetting.barricadeLengthHalf * 2);
                if (value != -1)
                {
                    spawnPos.x = value;
                    return barricadeSpawner.Spawn(spawnPos, onZAxis);
                }
            }
            return null;
        }
        private GameObject GenerateCollectable()
        {
            int s = Random.Range(0, 3);
            switch (s)
            {
                case 0:
                    return GenerateCoin();
                case 1:
                    return GenerateGasStation();
                case 2:
                    return GeneratePowerUp();
                default:
                    return null;
            }
        }
        private GameObject GenerateCriterlessCollectable()
        {

            return GenerateCoin();

        }
        private GameObject GenerateCoin()
        {
            if (onZAxis)
            {
                float value = roadSegmentation.AllocateSpace(setting.coinSetting.coinsLengthHalf * 2);
                if (value != -1)
                {
                    spawnPos.z = value;
                    return coinSpawner.Spawn(spawnPos, onZAxis);
                }
            }
            else
            {
                float value = roadSegmentation.AllocateSpace(setting.coinSetting.coinsLengthHalf * 2);
                if (value != -1)
                {
                    spawnPos.x = value;
                    return coinSpawner.Spawn(spawnPos, onZAxis);
                }
            }
            return null;
        }
        private GameObject GeneratePowerUp()
        {
            if (ProceduralRoadGenerator.instance.RoadIndex >= setting.powerUpSetting.powerUpsWillStartFrom
                && lastPowerUpCreatedIndex + setting.powerUpSetting.minPowerUpFrequency <= ProceduralRoadGenerator.instance.RoadIndex)
            {
                lastPowerUpCreatedIndex = ProceduralRoadGenerator.instance.RoadIndex;
                if (onZAxis)
                {
                    float value = roadSegmentation.AllocateSpace(setting.powerUpSetting.powerUpLengthHalf * 2);
                    if (value != -1)
                    {
                        spawnPos.z = value;
                        return powerUpSpawner.Spawn(spawnPos, onZAxis);
                    }
                }
                else
                {
                    float value = roadSegmentation.AllocateSpace(setting.powerUpSetting.powerUpLengthHalf * 2);
                    if (value != -1)
                    {
                        spawnPos.x = value;
                        return powerUpSpawner.Spawn(spawnPos, onZAxis);
                    }
                }
            }
            return null;
        }
        private GameObject GenerateGasStation()
        {
            if (ProceduralRoadGenerator.instance.RoadIndex >= setting.gasStationSetting.gasStationSpawnAfter
                && roadSegmentation.RoadLength >= setting.gasStationSetting.gasStationLengthHalf * 3
                && roadSegmentation.RemainingLength >= setting.gasStationSetting.gasStationLengthHalf * 2
                && lastGasStationCreatedIndex + setting.gasStationSetting.minGasStationFrequency <= ProceduralRoadGenerator.instance.RoadIndex)
            {
                lastGasStationCreatedIndex = ProceduralRoadGenerator.instance.RoadIndex;
                if (onZAxis)
                {
                    float value = roadSegmentation.AllocateSpace(setting.gasStationSetting.gasStationLengthHalf * 2);
                    if (value != -1)
                    {
                        spawnPos.z = value;
                        return gasStationSpawner.Spawn(spawnPos, onZAxis);
                    }
                }
                else
                {
                    float value = roadSegmentation.AllocateSpace(setting.gasStationSetting.gasStationLengthHalf * 2);
                    if (value != -1)
                    {
                        spawnPos.x = value;
                        return gasStationSpawner.Spawn(spawnPos, onZAxis);
                    }
                }
                return null;
            }
            else
            {
                return GenerateCriterlessCollectable();
            }
        }
        private void GenerateEmptySpace()
        {
            float length = Random.Range(setting.minEmptySpaceLength, setting.maxEmptySpaceLength);
            roadSegmentation.AllocateSpace(length);
        }
        private int ObjectCountAccordingToRoadLength(float length)
        {
            RoadObjectsSetting.RoadLengthMinMax[] minMaxes = setting.roadObjectCounts;
            for (int i = 0; i < minMaxes.Length-1; i++)
            {
                if (minMaxes[i].length <= length && minMaxes[i+1].length > length)
                    return Random.Range(minMaxes[i].min, minMaxes[i].max);
            }
            return Random.Range(minMaxes[minMaxes.Length-1].min, minMaxes[minMaxes.Length - 1].max);
        }
    }
}
