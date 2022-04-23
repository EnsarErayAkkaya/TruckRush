using Project.Settings;
using Project.Spawners;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Road
{
    public class OffRoadObjectsGenerator
    {
        private OffRoadObjectsSetting setting;

        #region spawners
        private GasStationSpawner gasStationSpawner;
        private StreetLightSpawner streetLightSpawner;
        private OffroadObjectSpawner offroadObjectSpawner;
        #endregion

        private RoadSegmentation offRoadSegmentation;
        private Vector3 spawnPos;
        private bool onZAxis;

        //Gas Station
        private int nextGasStationSpawnIndex;
        private OffRoadObjectsSetting.BiomType currentBiomType;
        private Road road;
        private float roadWidthHalf;
        private float remainingLengthForSpace;

        public OffRoadObjectsGenerator(OffRoadObjectsSetting setting)
        {
            this.setting = setting;
            gasStationSpawner = new GasStationSpawner(setting.gasStationSetting);
            offroadObjectSpawner = new OffroadObjectSpawner(setting);
            streetLightSpawner = new StreetLightSpawner(setting.streetLightSetting);

            nextGasStationSpawnIndex = setting.gasStationSetting.gasStationSpawnAfter;
            offRoadSegmentation = new RoadSegmentation();
            // set biom type
            currentBiomType = OffRoadObjectsSetting.BiomType.Village;
            offroadObjectSpawner.Set(setting.bioms.Find(s => s.biomType == currentBiomType));
            roadWidthHalf = ProceduralRoadGenerator.instance.Setting.roadWidthHalf;
        }

        public void Set(Road road, bool onZAxis, float startingPoint, float endingPoint)
        {
            this.road = road;
            this.onZAxis = onZAxis;

            GenerateStreetLights(startingPoint, endingPoint);

            if (onZAxis)
            {
                startingPoint -= roadWidthHalf * 2;
                //endingPoint -= roadWidthHalf * 2;
                spawnPos = new Vector3(road.transform.position.x, 0, startingPoint);
            }
            else
            {
                // Move starting point and end point to back to align with road visualy
                startingPoint += roadWidthHalf * 2;
                endingPoint += roadWidthHalf * 2;
                spawnPos = new Vector3(startingPoint, 0, road.transform.position.z);
            }

            offRoadSegmentation.Set(startingPoint, endingPoint);

            // will generate gas station
            if(ProceduralRoadGenerator.instance.RoadIndex >= nextGasStationSpawnIndex)
            {
                GasStationCreationSequence();
                nextGasStationSpawnIndex += Random.Range(
                    setting.gasStationSetting.minGasStationFrequency,
                    setting.gasStationSetting.maxGasStationFrequency);
            }

        }
        private void GasStationCreationSequence()
        {
            // what is the remaining length when we create gas station
            float remainingLengthAfterGas = offRoadSegmentation.RoadLength - setting.gasStationSetting.gasStationLengthHalf * 2;

            float allocatedSpace = Random.Range(0, remainingLengthAfterGas);
            if(offRoadSegmentation.AllocateSpace(allocatedSpace) != -1)
            {
                GenerateGasStation();
            }
        }
        private void GenerateGasStation()
        {
            if (onZAxis)
            {
                float value = offRoadSegmentation.AllocateSpace(setting.gasStationSetting.gasStationLengthHalf * 2);
                if (value != -1)
                {
                    spawnPos.z = value;
                    road.AddObject(gasStationSpawner.Spawn(spawnPos, onZAxis));
                }
            }
            else
            {
                float value = offRoadSegmentation.AllocateSpace(setting.gasStationSetting.gasStationLengthHalf * 2);
                if (value != -1)
                {
                    spawnPos.x = value;
                    road.AddObject(gasStationSpawner.Spawn(spawnPos, onZAxis));
                }
            }
        }
        private void GenerateStreetLights(float startingPos, float endingPos)
        {
            if (onZAxis)
            {
                endingPos += roadWidthHalf * 2;
            }
            else
            {
                startingPos -= roadWidthHalf * 2;
            }

            float curr = startingPos + Random.Range(0, setting.streetLightSetting.maxStartingSpace);
            Vector3 lightPos;
            while ( curr < endingPos)
            {
                if (onZAxis)
                {
                    lightPos = new Vector3(road.transform.position.x, 0, curr);
                }
                else
                {
                    lightPos = new Vector3(curr, 0, road.transform.position.z);
                }
                road.AddObject(streetLightSpawner.Spawn(lightPos, onZAxis));
                curr += setting.streetLightSetting.streetLightSpawnFreaquency;
            }
        }
    }
}