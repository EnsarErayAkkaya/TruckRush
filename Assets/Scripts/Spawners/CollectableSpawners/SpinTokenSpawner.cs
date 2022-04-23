using Project.Road;
using Project.Settings.Collectables;
using UnityEngine;

namespace Project.Spawners
{
    class SpinTokenSpawner : SpawnerBase<SpinTokenSetting>
    {
        public SpinTokenSpawner(SpinTokenSetting setting) : base(setting)
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
            pos.y += setting.spinTokenHeightHalf;
            return Object.Instantiate(setting.spinTokenPrefab, pos, q);
        }
    }
}
