using Project.Player;
using Project.UI;
using System;
using System.Collections;
using UnityEngine;

namespace Project.GameSystems
{
    public class ScoreManager : MonoBehaviour
    {
        private float distanceTravelled; // trucks current traveld distance
        [SerializeField] private float[] distanceMilestones;

        [SerializeField] private TruckMovement truck;

        private Vector3 startingPos;
        private WaitForSeconds w = new WaitForSeconds(1); // just didnt want to create every time
        private int lastMileStoneIndex;
        private void Start()
        {
            GameManager.instance.onGameStart += StartScoring;   
        }
        private void StartScoring()
        {
            startingPos = truck.transform.position;
            StartCoroutine(CheckDistance());
        }

        private IEnumerator CheckDistance()
        {
            while(truck.CanMove)
            {
                distanceTravelled = Vector3.Distance(truck.transform.position, startingPos);
                CheckMilestones();
                yield return w;
            }
        }

        private void CheckMilestones()
        {
            if(distanceMilestones.Length > lastMileStoneIndex && distanceMilestones[lastMileStoneIndex] <= distanceTravelled)
            {
                Notification.instance.AddNotification("You Travelled for " + distanceMilestones[lastMileStoneIndex] + " Meter");
                lastMileStoneIndex++;
            } 
        }
    }
}