using Project.Collectables;
using Project.Road;
using Project.Settings;
using UnityEngine;

namespace Project.Spawners
{
    class StreetLightSpawner : SpawnerBase<StreetLightSetting>
    {
        public StreetLightSpawner(StreetLightSetting setting) : base(setting)
        {
        }
        public override GameObject Spawn(Vector3 spawnLine, bool onZAxis)
        {
            float roadWidthHalf = ProceduralRoadGenerator.instance.Setting.roadWidthHalf;
            Vector3 pos = spawnLine;
            Quaternion q = Quaternion.identity;
            if (onZAxis) // means road on Z Axis
            {
                pos.x -= roadWidthHalf + setting.streetLightWidth;
            }
            else// means road on X Axis
            {
                pos.z += roadWidthHalf + setting.streetLightWidth;
                q = Quaternion.Euler(0, 90, 0);
            }
            pos.y += setting.streetLightHeight; 
            return Object.Instantiate(setting.streetLightPrefab, pos, q);
        }
    }
}
