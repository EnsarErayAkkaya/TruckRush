using Project.PowerUps;
using System.Collections.Generic;

namespace Project.Settings
{
    [System.Serializable]
    public class GameData
    {
        public float highScore;
        public int credit;
        public List<string> powerUpLevelNames;
        public List<int> powerUpLevels;
        public int truckIndex;
        public int wingIndex;
        public List<string> ownedTrucks;
        public List<string> ownedWings;
        public int headStartCount;
        public int spinTokenCount;
        public bool isPlayedBefore;
        public bool isTruckLayoutHidden;
        public bool isSoundsOff;
        public GameData()
        {
            powerUpLevelNames = new List<string>();
            powerUpLevels = new List<int>();
            ownedTrucks = new List<string>();
            ownedWings = new List<string>();
            isPlayedBefore = false;
            isTruckLayoutHidden = false;
        }

    }
}
