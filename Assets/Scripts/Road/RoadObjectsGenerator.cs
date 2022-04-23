using Project.GameSystems;
using Project.Settings;
using Project.Spawners;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Road
{
    public class RoadObjectsGenerator : MonoBehaviour
    {
        [SerializeField] private RoadObjectsSetting setting;
        [SerializeField] private OffRoadObjectsSetting offRoadSetting;

        #region Spawners
        private BarricadeSpawner barricadeSpawner;
        private BigBarricadeSpawner bigBarricadeSpawner;
        private CoinSpawner coinSpawner;
        private PowerUpSpawner powerUpSpawner;
        private DistanceMilestoneSpawner distanceMilestoneSpawner;
        private CrossroadSpawner crossroadSpawner;
        private WrenchSpawner wrenchSpawner;
        private SpinTokenSpawner spinTokenSpawner;
        #endregion

        private OffRoadObjectsGenerator offRoadObjectsGenerator;
        private RoadSegmentation roadSegmentation;

        private float startingPoint;
        private float endingPoint;

        private Vector3 spawnPos;
        private bool onZAxis;
        // PowerUp
        private int lastPowerUpCreatedIndex;
        // Milestone
        private float nextMileStone;
        //Big barricade
        private int lastBigBarricadeSpawnIndex;
        //Milestone
        public float totalLength;
        //Crossroad
        private int lastCrossroadSpawnIndex = -1;
        private int crossroadCountOnRoad;
        //Wrench
        private int lastWrenchSpawnIndex = 0;
        //Spin Token
        private int lastSpinTokenSpawnIndex = 0;
        private void Awake()
        {
            roadSegmentation = new RoadSegmentation();
            offRoadObjectsGenerator = new OffRoadObjectsGenerator(offRoadSetting);

            barricadeSpawner = new BarricadeSpawner(setting.barricadeSetting);
            bigBarricadeSpawner = new BigBarricadeSpawner(setting.bigBarricadeSetting);
            coinSpawner = new CoinSpawner(setting.coinSetting);
            powerUpSpawner = new PowerUpSpawner(setting.powerUpSetting);
            distanceMilestoneSpawner = new DistanceMilestoneSpawner(setting.distanceMilestoneSetting);
            crossroadSpawner = new CrossroadSpawner(setting.crossroadSetting);
            wrenchSpawner = new WrenchSpawner(setting.wrenchSetting);
            spinTokenSpawner = new SpinTokenSpawner(setting.spinTokenSetting);
            nextMileStone = ScoreManager.instance.GetNextMilestone();
        }

        public void GenerateRoadObjects(Road road, bool onZAxis, float roadLength)
        {
            this.onZAxis = onZAxis;
            float roadWidthHalf = ProceduralRoadGenerator.instance.Setting.roadWidthHalf;

            // Reset resetable data
            crossroadCountOnRoad = 0;
            
            if (onZAxis)
            {
                startingPoint = road.transform.position.z - roadLength / 2 - roadWidthHalf;
                endingPoint = road.transform.position.z + roadLength / 2 - roadWidthHalf;
                spawnPos = new Vector3(road.transform.position.x, 0, startingPoint);
            }
            else
            {
                startingPoint = road.transform.position.x - roadLength / 2 - roadWidthHalf;
                endingPoint = road.transform.position.x + roadLength / 2 - roadWidthHalf;
                spawnPos = new Vector3(startingPoint, 0, road.transform.position.z);
            }

            offRoadObjectsGenerator.Set(road, onZAxis, startingPoint, endingPoint);

            // Generate Milestone
            if (totalLength + roadLength >= nextMileStone)
            {
                // spawn milestone object
                Vector3 pos = spawnPos;
                if (onZAxis)
                    pos.z += nextMileStone - totalLength;
                else
                    pos.x += nextMileStone - totalLength;

                distanceMilestoneSpawner.Set(nextMileStone);
                road.AddObject(distanceMilestoneSpawner.Spawn(pos, onZAxis));
                nextMileStone = ScoreManager.instance.GetNextMilestone();
            }
            totalLength += roadLength;

            roadSegmentation.Set(startingPoint, endingPoint);

            GenerateEmptySpace();

            int objectCount = ObjectCountAccordingToRoadLength(roadLength);

            for (int i = 0; i < objectCount; i++)
            {
                int code = Random.Range(0, 3);
                GameObject g;
                switch (code)
                {
                    case 0:
                         g = GenerateObstacle();
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
        private GameObject GenerateObstacle()
        {
            int s = Random.Range(0, 3);
            switch (s)
            {
                case 0:
                    return GenerateBarricade();
                case 1:
                    return GenerateBigBarricade();
                case 2:
                    return GenerateCrossroad();
                default:
                    return null;
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
        private GameObject GenerateBigBarricade()
        {
            if (ProceduralRoadGenerator.instance.RoadIndex >= setting.bigBarricadeSetting.bigBarricadeSpawnAfter
                && lastBigBarricadeSpawnIndex + setting.bigBarricadeSetting.bigBarricadeSpawnFreaquency < ProceduralRoadGenerator.instance.RoadIndex)
            {
                lastBigBarricadeSpawnIndex = ProceduralRoadGenerator.instance.RoadIndex;
                if (onZAxis)
                {
                    float value = roadSegmentation.AllocateSpace(setting.bigBarricadeSetting.bigBarricadeLengthHalf * 2);
                    if (value != -1)
                    {
                        spawnPos.z = value;
                        return bigBarricadeSpawner.Spawn(spawnPos, onZAxis);
                    }
                }
                else
                {
                    float value = roadSegmentation.AllocateSpace(setting.bigBarricadeSetting.bigBarricadeLengthHalf * 2);
                    if (value != -1)
                    {
                        spawnPos.x = value;
                        return bigBarricadeSpawner.Spawn(spawnPos, onZAxis);
                    }
                }
            }
            return null;
        }
        private GameObject GenerateCrossroad()
        {
            int roadIndex = ProceduralRoadGenerator.instance.RoadIndex;
            if (roadIndex >= setting.crossroadSetting.crossroadSpawnAfter
                && lastCrossroadSpawnIndex + setting.crossroadSetting.crossroadSpawnFreaquency < roadIndex
                 || lastCrossroadSpawnIndex == roadIndex && crossroadCountOnRoad < setting.crossroadSetting.maxCrossroadOnRoad)
            {
                lastCrossroadSpawnIndex = ProceduralRoadGenerator.instance.RoadIndex;
                crossroadCountOnRoad++;
                if (onZAxis)
                {
                    float value = roadSegmentation.AllocateSpace(setting.bigBarricadeSetting.bigBarricadeLengthHalf * 2);
                    if (value != -1)
                    {
                        spawnPos.z = value;
                        return crossroadSpawner.Spawn(spawnPos, onZAxis);
                    }
                }
                else
                {
                    float value = roadSegmentation.AllocateSpace(setting.bigBarricadeSetting.bigBarricadeLengthHalf * 2);
                    if (value != -1)
                    {
                        spawnPos.x = value;
                        return crossroadSpawner.Spawn(spawnPos, onZAxis);
                    }
                }
            }
            return null;
        }
        private GameObject GenerateCollectable()
        {
            int s = Random.Range(0, 4);
            switch (s)
            {
                case 0:
                    return GenerateCoin();
                case 1:
                    return GeneratePowerUp();
                case 2:
                    return GenerateWrench();
                case 3:
                    return GenerateSpinToken();
                default:
                    return null;
            }
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
        private GameObject GenerateWrench()
        {
            int roadIndex = ProceduralRoadGenerator.instance.RoadIndex;
            if (roadIndex >= setting.wrenchSetting.wrenchWillStartFrom &&
                lastWrenchSpawnIndex + setting.wrenchSetting.minWrenchFrequency < roadIndex)
            {
                lastWrenchSpawnIndex = roadIndex;
                if (onZAxis)
                {
                    float value = roadSegmentation.AllocateSpace(setting.wrenchSetting.wrenchLengthHalf * 2);
                    if (value != -1)
                    {
                        spawnPos.z = value;
                        return wrenchSpawner.Spawn(spawnPos, onZAxis);
                    }
                }
                else
                {
                    float value = roadSegmentation.AllocateSpace(setting.wrenchSetting.wrenchLengthHalf * 2);
                    if (value != -1)
                    {
                        spawnPos.x = value;
                        return wrenchSpawner.Spawn(spawnPos, onZAxis);
                    }
                }
            }
            return null;
        }
        private GameObject GenerateSpinToken()
        {
            int roadIndex = ProceduralRoadGenerator.instance.RoadIndex;
            if (roadIndex >= setting.spinTokenSetting.tokenWillStartFrom &&
                lastSpinTokenSpawnIndex + setting.spinTokenSetting.minTokenFrequency < roadIndex)
            {
                lastSpinTokenSpawnIndex = roadIndex;
                if (onZAxis)
                {
                    float value = roadSegmentation.AllocateSpace(setting.spinTokenSetting.spinTokenLengthHalf * 2);
                    if (value != -1)
                    {
                        spawnPos.z = value;
                        return spinTokenSpawner.Spawn(spawnPos, onZAxis);
                    }
                }
                else
                {
                    float value = roadSegmentation.AllocateSpace(setting.spinTokenSetting.spinTokenLengthHalf * 2);
                    if (value != -1)
                    {
                        spawnPos.x = value;
                        return spinTokenSpawner.Spawn(spawnPos, onZAxis);
                    }
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
