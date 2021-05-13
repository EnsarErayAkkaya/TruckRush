using UnityEngine;

namespace Project.Spawners
{
    class SpawnerBase<T>
    {
        protected T setting;

        public SpawnerBase(T setting)
        {
            this.setting = setting;
        }

        public virtual GameObject Spawn(Vector3 spawnLine, bool onZAxis)
        {
            Debug.Log("Spawner Not Implemented");
            return null;
        }
    }
}
