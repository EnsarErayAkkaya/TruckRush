using Project.Collectables;
using Project.Road;
using Project.Settings.Collectables;
using UnityEngine;

namespace Project.Spawners
{
    class GasStationSpawner : SpawnerBase<GasStationSetting>
    {
        public GasStationSpawner(GasStationSetting setting) : base(setting)
        {
        }
        public override GameObject Spawn(Vector3 spawnLine, bool onZAxis)
        {
            float roadWidthHalf = ProceduralRoadGenerator.instance.Setting.roadWidthHalf;
            Vector3 pos = spawnLine;
            Quaternion q = Quaternion.identity;
            if (onZAxis) // means road on Z Axis
            {
                pos.x += roadWidthHalf;
            }
            else// means road on X Axis
            {
                pos.z -= roadWidthHalf;
                q = Quaternion.Euler(0, 90, 0);
            }
            pos.y -= setting.gasStationHeightHalf;
            GameObject g = Object.Instantiate(setting.gasStationPrefab, pos, q);
            g.GetComponent<GasStation>().Set(Random.Range(setting.minRequiredCoinValue, setting.maxRequiredCoinValue),
                Random.Range(setting.minGasValue, setting.maxGasValue));
            return g;
        }
    }
}
