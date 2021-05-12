using Project.Road;
using Project.Settings.Obstacles;
using UnityEngine;

namespace Project.Spawners
{
    class BarricadeSpawner : SpawnerBase<BarricadeSetting>
    {
        public BarricadeSpawner(BarricadeSetting setting) : base(setting)
        {
        }
        public override void Spawn(Vector3 spawnLine, bool onZAxis)
        {
            float roadWidthHalf = ProceduralRoadGenerator.instance.Setting.roadWidthHalf; 
            int barricadeSide = Random.Range(-1, 1) < 0 ? -1 : 1; // will baricade be on left or right of road
            Debug.Log("barricadeSide: " + barricadeSide);
            Vector3 pos = spawnLine;
            Quaternion q = Quaternion.identity;
            if(onZAxis) // means road on Z Axis
            {
                pos.x += roadWidthHalf * barricadeSide + setting.barricadeWidthHalf * -barricadeSide;
            }
            else// means road on X Axis
            {
                pos.z += roadWidthHalf * barricadeSide + setting.barricadeWidthHalf * -barricadeSide;
                q = Quaternion.Euler(0, 90, 0);
            }
            pos.y += setting.barricadeHeightHalf;
            Object.Instantiate(setting.barricadePrefab, pos, q);
        }
    }
}
