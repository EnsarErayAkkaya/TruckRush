using Project.Obstacles;
using Project.Road;
using Project.Settings;
using UnityEngine;

namespace Project.Spawners
{
    class BigBarricadeSpawner : SpawnerBase<BigBarricadeSetting>
    {
        public BigBarricadeSpawner(BigBarricadeSetting setting) : base(setting)
        {
        }
        public override GameObject Spawn(Vector3 spawnLine, bool onZAxis)
        {
            int barricadeSide = Random.Range(-1, 1) < 0 ? -1 : 1; // will baricade be on left or right of road
            Vector3 pos = spawnLine;
            Quaternion q = Quaternion.identity;
            if (onZAxis) // means road on Z Axis
            {
            }
            else// means road on X Axis
            {
                q = Quaternion.Euler(0, 90, 0);
            }
            pos.y += setting.bigBarricadeHeightHalf;
            GameObject g =  Object.Instantiate(setting.bigBarricadePrefab, pos, q);
            g.GetComponent<BigBarricade>().Set(barricadeSide);
            return g;
        }
    }
}
