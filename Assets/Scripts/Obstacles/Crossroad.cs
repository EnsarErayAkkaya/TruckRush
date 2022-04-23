using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Obstacles
{
    public class Crossroad : MonoBehaviour
    {
        [SerializeField] private Pedestrian pedestrian;
        [SerializeField] private Transform point1;
        [SerializeField] private Transform point2;
        [SerializeField] private float pedesterianSpeed;

        public Pedestrian Pedestrian => pedestrian;

        private void Start()
        {
            StartCoroutine(EnumeratePedesterian());
            pedestrian.transform.position = new Vector3(Random.Range(point2.position.x, point1.position.x), point1.position.y, point1.position.z);
        }
        public void Set(bool onZAxis)
        {

        }

        private IEnumerator EnumeratePedesterian()
        {
            Vector3 goal = point2.position;
            while (pedestrian != null)
            {
                if (pedestrian.transform.position == point1.position)
                {
                    pedestrian.transform.localRotation = Quaternion.Euler(0, -90, 0);
                    goal = point2.position;
                }
                else if (pedestrian.transform.position == point2.position)
                {
                    pedestrian.transform.localRotation = Quaternion.Euler(0, 90, 0);
                    goal = point1.position;
                }
                pedestrian.transform.position = Vector3.MoveTowards(pedestrian.transform.position, goal, Time.deltaTime * pedesterianSpeed);
                yield return null;
            }
        }
    }
}