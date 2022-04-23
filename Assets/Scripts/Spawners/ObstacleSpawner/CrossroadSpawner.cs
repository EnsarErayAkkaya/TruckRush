using Project.Obstacles;
using Project.Settings.Obstacles;
using UnityEngine;

namespace Project.Spawners
{
    class CrossroadSpawner : SpawnerBase<CrossroadSetting>
    {
        public CrossroadSpawner(CrossroadSetting setting) : base(setting)
        {
        }

        public override GameObject Spawn(Vector3 spawnLine, bool onZAxis)
        {
            Vector3 pos = spawnLine;
            Quaternion q = Quaternion.identity;
            if (onZAxis) // means road on Z Axis
            {
            }
            else// means road on X Axis
            {
                q = Quaternion.Euler(0, 90, 0);
            }
            pos.y = setting.crossroadHeight;
            GameObject g = Object.Instantiate(setting.crossroadPrefab, pos, q);
            g.GetComponent<Crossroad>().Pedestrian.Set(setting.minDamage, setting.maxDamage);
            return g;
        }
    }
}
