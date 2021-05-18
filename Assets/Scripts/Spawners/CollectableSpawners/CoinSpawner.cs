using Project.Road;
using Project.Settings.Collectables;
using UnityEngine;

namespace Project.Spawners
{
    class CoinSpawner : SpawnerBase<CoinSetting>
    {
        public CoinSpawner(CoinSetting setting) : base(setting)
        {
        }
        public override GameObject Spawn(Vector3 spawnLine, bool onZAxis)
        {
            float roadWidthHalf = ProceduralRoadGenerator.instance.Setting.roadWidthHalf;
            float barricadeSide = Random.Range(-1f, 1f); // will baricade be on left or right of road
            Vector3 pos = spawnLine;
            Quaternion q = Quaternion.identity;
            if (onZAxis) // means road on Z Axis
            {
                pos.x += roadWidthHalf * barricadeSide;
            }
            else// means road on X Axis
            {
                pos.z += roadWidthHalf * barricadeSide;
                q = Quaternion.Euler(0, 90, 0);
            }
            pos.y += setting.coinsHeightHalf;
            return Object.Instantiate(setting.CoinPrefab, pos, q);
        }
    }
}
