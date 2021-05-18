using Project.Collectables;
using Project.Road;
using Project.Settings.Collectables;
using UnityEngine;

namespace Project.Spawners
{
    class PowerUpSpawner : SpawnerBase<PowerUpSetting>
    {
        public PowerUpSpawner(PowerUpSetting setting) : base(setting)
        {
        }
        public override GameObject Spawn(Vector3 spawnLine, bool onZAxis)
        {
            float roadWidthHalf = ProceduralRoadGenerator.instance.Setting.roadWidthHalf;
            float barricadeSide = Random.Range(-1f, 1f); // will baricade be on left or right of road
            Vector3 pos = spawnLine;
            if (onZAxis) // means road on Z Axis
            {
                pos.x += roadWidthHalf * barricadeSide;
            }
            else// means road on X Axis
            {
                pos.z += roadWidthHalf * barricadeSide;
            }
            pos.y += setting.powerUpHeightHalf;
            return Object.Instantiate(setting.PowerUpPrefab, pos, Quaternion.identity);
        }
    }
}
