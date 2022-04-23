using Project.Player;
using Project.Road;
using Project.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.GameSystems
{
    public class TruckManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] trucks;
        [SerializeField] private TruckInterface[] truckInterfaces;
        [SerializeField] private Mesh[] wings;
        [SerializeField] private Material[] wingMaterials;
        [SerializeField] private float[] speeds;

        private GameObject selectedTruck;
        private Mesh selectedWing;
        private Material selectedWingMaterial;
        private int truckIndex;
        private int wingIndex;
        private List<string> ownedTrucks;
        private List<string> ownedWings;

        private TruckMovement truckMovement;

        private int headStartCount;

        public Transform truck;
        public int TruckIndex => truckIndex;
        public int WingIndex => wingIndex;
        public int HeadStartCount => headStartCount;

        public static TruckManager instance;

        [System.Serializable]
        public struct TruckInterface
        {
            public Sprite steeringWheel;
            public Sprite truckLayout;
        }
        
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this);
            
        }
        private void Start()
        {
            truckIndex = DataManager.instance.savedData.truckIndex;
            wingIndex = DataManager.instance.savedData.wingIndex;

            ownedTrucks = DataManager.instance.savedData.ownedTrucks;
            ownedWings = DataManager.instance.savedData.ownedWings;

            if(ownedTrucks.Count < 1)
            {
                BuyTruck("BasicTruck"); // normal name have space but these doesn't have. I dont know why
                BuyWing("PlaneWing");
            }

            headStartCount = DataManager.instance.savedData.headStartCount;

            selectedTruck = trucks[truckIndex];
            selectedWing = wings[wingIndex];
            selectedWingMaterial = wingMaterials[wingIndex];
        }
        public void HeadStartActivate()
        {
            if (headStartCount > 0 && !GameManager.instance.resurrected)
            {
                PowerUpManager.instance.UsePowerUp("fly");
                headStartCount--;
                DataManager.instance.savedData.headStartCount = headStartCount;
                DataManager.instance.Save();
            }
        }
        public void AddHeadStart()
        {
            headStartCount++;
            DataManager.instance.savedData.headStartCount++;
            DataManager.instance.Save();
        }

        #region Truck
        public void SelectTruck(int index)
        {
            truckIndex = index;
            selectedTruck = trucks[index];
            DataManager.instance.savedData.truckIndex = truckIndex;
            DataManager.instance.Save();
        }
        public void BuyTruck(string truckName)
        {
            ownedTrucks.Add(truckName);
            DataManager.instance.savedData.ownedTrucks = ownedTrucks;
            DataManager.instance.Save();
        }
        public bool IsTruckOwned(string truckName)
        {
            return ownedTrucks.Any(s => s == truckName);
        }
        #endregion
        #region Wing
        public void SelectWing(int index)
        {
            wingIndex = index;
            selectedWing = wings[wingIndex];
            selectedWingMaterial = wingMaterials[wingIndex];
            DataManager.instance.savedData.wingIndex = wingIndex;
            DataManager.instance.Save();
        }
        public void BuyWing(string wingName)
        {
            ownedWings.Add(wingName);
            DataManager.instance.savedData.ownedWings = ownedWings;
            DataManager.instance.Save();
        }
        public bool IsWingOwned(string wingName)
        {
            return ownedWings.Any(s => s == wingName);
        }
        #endregion

        public void CreateTruck()
        {
            
            GameManager.instance.onGameStart += HeadStartActivate;

            GameObject g = Instantiate(selectedTruck);
            g.GetComponent<TruckWings>().SetWingMeshes(selectedWing, selectedWingMaterial);

            // for test the selected truck
            //GameObject g1 = Instantiate(trucks[0]); 
            //g1.GetComponent<TruckWings>().SetWingMeshes(selectedWing, selectedWingMaterial);
            TruckInterface tI = truckInterfaces[truckIndex];

            FindObjectOfType<GameUI>().SetTruckLayoutAndSteeringWheel(tI.steeringWheel, tI.truckLayout);

            truck = g.transform.GetChild(0);
            truckMovement = truck.GetComponent<TruckMovement>();
        }
        public void TransportTruckToClosestRoad()
        {
            Road.Road r = ProceduralRoadGenerator.instance.GetClosestRoad(truck.position);
            Vector3 v = new Vector3(r.transform.position.x, truck.position.y, r.transform.position.z);
            Vector3 dir = v - truck.position;
            if (r.OnZAxis)
            {
                truck.rotation = Quaternion.identity;
                truck.position += dir;

                Transform back = truck.parent.GetChild(1);
                back.rotation = truck.rotation;
                back.position = new Vector3(truck.position.x, truck.position.y, truck.position.z - 6.23f);
            }
            else
            {
                truck.rotation = Quaternion.Euler(0,90,0);
                truck.position += dir;

                Transform back = truck.parent.GetChild(1);
                back.rotation = Quaternion.Euler(0, 90, 0);
                back.position = new Vector3(truck.position.x - 6.23f, truck.position.y, truck.position.z );
            }

        }

        public void SetTrucksSpeed(int index)
        {
            if(index < speeds.Length)
                truckMovement.SetSpeed(speeds[index]);
        }
    }
}