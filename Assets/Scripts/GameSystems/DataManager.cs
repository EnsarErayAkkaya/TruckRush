using Project.Settings;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Project.GameSystems
{
    public class DataManager : MonoBehaviour
    {
        public GameData savedData = new GameData();

        public static DataManager instance;
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
            Load();

            DontDestroyOnLoad(this.gameObject);
        }

        //it's static so we can call it from anywhere
        public void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
            FileStream file = File.Create(Application.persistentDataPath + "/gameData.gd"); //you can call it anything you want
            bf.Serialize(file, savedData);
            Debug.Log("Game saved");
            file.Close();
        }

        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/gameData.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/gameData.gd", FileMode.Open);
                savedData = (GameData)bf.Deserialize(file);
                file.Close();
            }
        }
    }
}
