using Project.Obstacles;
using Project.Road;
using Project.Settings.Obstacles;
using UnityEngine;
using Project.Settings.Collectables;
using Project.Collectables;

namespace Project.Spawners
{
    class DistanceMilestoneSpawner : SpawnerBase<DistanceMilestoneSetting>
    {
        private float dist;
        public DistanceMilestoneSpawner(DistanceMilestoneSetting setting) : base(setting)
        {
        }
        public void Set(float dist)
        {
            this.dist = dist;
        }
        public override GameObject Spawn(Vector3 spawnLine, bool onZAxis)
        {
            Vector3 pos = spawnLine;
            Quaternion q = Quaternion.identity;
            if(onZAxis) // means road on Z Axis
            {
                q = Quaternion.Euler(90, 0, 0);
            }
            else// means road on X Axis
            {
                q = Quaternion.Euler(90, 0, -90);
            }
            pos.y += setting.milestoneHeightHalf;
            GameObject g = Object.Instantiate(setting.milestonePrefab, pos, q);
            g.GetComponent<DistanceMilestone>().Set(dist);
            return g;
        }
    }
}
