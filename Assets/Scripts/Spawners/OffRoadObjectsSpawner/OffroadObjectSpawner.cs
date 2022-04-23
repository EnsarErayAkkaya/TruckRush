using Project.Collectables;
using Project.Road;
using Project.Settings;
using UnityEngine;

namespace Project.Spawners
{
    class OffroadObjectSpawner : SpawnerBase<OffRoadObjectsSetting>
    {
        private OffRoadObjectsSetting.Biom biom;
        public OffroadObjectSpawner(OffRoadObjectsSetting setting) : base(setting)
        {
        }
        public void Set(OffRoadObjectsSetting.Biom biom)
        {
            this.biom = biom;
        }
        public override GameObject Spawn(Vector3 spawnLine, bool onZAxis)
        {
            float roadWidthHalf = ProceduralRoadGenerator.instance.Setting.roadWidthHalf;
            Vector3 pos = spawnLine;
            Quaternion q = Quaternion.identity;
            if (onZAxis) // means road on Z Axis
            {
                pos.x += roadWidthHalf + setting.offRoadObjectWidth;
            }
            else// means road on X Axis
            {
                pos.z -= roadWidthHalf + setting.offRoadObjectWidth;
                q = Quaternion.Euler(0, 90, 0);
            }
            return Object.Instantiate(biom.biomObjects[Random.Range(0, biom.biomObjects.Length)], pos, q);
        }
    }
}
