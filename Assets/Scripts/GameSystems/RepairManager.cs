using Project.Player;
using Project.UI;
using UnityEngine;

namespace Project.GameSystems
{
    public class RepairManager : MonoBehaviour
    {
        private int repairObjectCount = 0;
        private PlayerHealth playerHealth;
        [SerializeField] private GameUI gameUI;
        public static RepairManager instance;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }
        public void Add() 
        {
            repairObjectCount++;
            CheckEnoughRepairObjectCollected();
            gameUI.SetWrenchText(repairObjectCount % 3);
        }
        private void CheckEnoughRepairObjectCollected()
        {
            if (repairObjectCount % 3 == 0)
            {
                playerHealth.Health += playerHealth.MaxHealth / 2;
                AudioManager.instance.Play("3WrenchCollected");
            }
        }
    }
}
