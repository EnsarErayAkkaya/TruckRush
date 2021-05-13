using System.Collections.Generic;
using UnityEngine;
using Project.Road;

namespace Project.Player
{
    public class PlayerRoad : MonoBehaviour
    {
        [SerializeField] private LayerMask roadLayer;

        private Transform road;

        private void FixedUpdate()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);
            if(Physics.Raycast(ray, out hit, 10, roadLayer))
            {
                if(hit.transform != null && hit.transform != road)
                {
                    road = hit.transform;
                    ProceduralRoadGenerator.instance.TruckRoadIndexSet(road.GetComponent<Road.Road>());
                }
            }
        }
    }
}
