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
        private int gainedCreditBeforeResurrection = 0;

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
            GameManager.instance.onResurrect += SetCreditBeforeResurrection;
        }

        private void SetCreditBeforeResurrection()
        {
            gainedCreditBeforeResurrection = GainedCredit;
        }

        public void AddSpinToken()
        {
            DataManager.instance.savedData.spinTokenCount++;
            DataManager.instance.Save();
        }

        public float GetNextMilestone()
        {
            if (current < distanceMilestones.Length)
            {
                float a = distanceMilestones[current];
                current++;
                return a;
            }
            return 0;
        }
        public void AchieveMilestone()
        {
            achievedMilestoneCount++;
            //Debug.Log("achievedMilestoneCount: " + achievedMilestoneCount);
            TruckManager.instance.SetTrucksSpeed(achievedMilestoneCount);
        }
        public void SetDistanceTravelled(float dist) 
        {
            distanceTravelled = dist; 
            CreditManager.instance.GainCredit(GainedCredit - gainedCreditBeforeResurrection);
            SaveHighScore();
        }
        public void SaveHighScore()
        {
            if (distanceTravelled > DataManager.instance.savedData.highScore) 
            {
                DataManager.instance.savedData.highScore = distanceTravelled;
                DataManager.instance.Save();
            }
        }
    }
}