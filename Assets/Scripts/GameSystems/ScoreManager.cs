using Project.Player;
using Project.UI;
using System;
using System.Collections;
using UnityEngine;

namespace Project.GameSystems
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private float[] distanceMilestones;

        [SerializeField] private TruckMovement truck;
        
        private int current = 0;
        private int achievedMilestoneCount = 0;

        private float distanceTravelled;
        private int usedPowerUpCount;

        public float DistanceTravelled => distanceTravelled;
        public float AchievedMilestoneCount => achievedMilestoneCount;
        
        public int UsedPowerUpCount
        {
            get => usedPowerUpCount;
            set
            {
                usedPowerUpCount = value;
            }
        }
        public int GainedCredit => (int)distanceTravelled +
                (int)(usedPowerUpCount * CreditManager.instance.PowerUpCreditMultiplier) +
                (int)(achievedMilestoneCount * CreditManager.instance.MilestoneCreditMultiplier);
        public static ScoreManager instance;
        private void Awake()
        {
            instance = this;
        }

        public float GetNextMilestone()
        {
            float a = distanceMilestones[current];
            current++;
            return a;
        }
        public void AchieveMilestone() => achievedMilestoneCount++;
        public void SetDistanceTravelled(float dist) 
        {
            distanceTravelled = dist; 
            CreditManager.instance.GainCredit(GainedCredit);
        }
    }
}