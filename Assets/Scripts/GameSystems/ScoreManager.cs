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
    }
}