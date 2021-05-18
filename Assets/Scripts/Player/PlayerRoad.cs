using System.Collections.Generic;
using UnityEngine;
using Project.Road;
using Project.GameSystems;

namespace Project.Player
{
    public class PlayerRoad : MonoBehaviour
    {
        [SerializeField] private LayerMask roadLayer;

        private Transform road;
        private Vector3 pos;
        private void Start()
        {
            GameManager.instance.onPlayerLost += OnGameEnded;
        }

        private void FixedUpdate()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);
            if(Physics.Raycast(ray, out hit, 10, roadLayer))
            {
                if(hit.transform != null && hit.transform != road)
                {
                    road = hit.transform;
                    pos = hit.transform.position;
                    ProceduralRoadGenerator.instance.TruckRoadIndexSet(road.GetComponent<Road.Road>());
                }
            }
        }
        private void OnGameEnded()
        {
            ScoreManager.instance.SetDistanceTravelled(road.GetComponent<Road.Road>().GetTotalLength(pos));
        }
    }
}
