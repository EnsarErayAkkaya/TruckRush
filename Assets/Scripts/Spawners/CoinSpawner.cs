using Project.Settings.Collectables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Spawners
{
    class CoinSpawner : SpawnerBase<CoinSetting>
    {
        public CoinSpawner(CoinSetting setting) : base(setting)
        {

        }
    }
}
