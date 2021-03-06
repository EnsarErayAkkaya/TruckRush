using System.Collections.Generic;
using UnityEngine;

namespace Project.Road
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private Transform roadSurface;
        private List<GameObject> roadObjects = new List<GameObject>();
        private Vector3 startingPos;
        private float totalLength;
        private bool onZAxis;

        public bool OnZAxis => onZAxis;
        public void Set(Vector3 startingPos, float totalLength, bool onZAxis)
        {
            this.startingPos = startingPos;
            this.totalLength = totalLength;
            this.onZAxis = onZAxis;
            if (this.onZAxis == false)
                roadSurface.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        public float GetTotalLength( Vector3 currentPos)
        {
            return totalLength + Vector3.Distance(startingPos, currentPos);
        }
        public void AddObject(GameObject g)
        {
            roadObjects.Add(g);
        }
        public void Clear()
        {
            foreach (GameObject item in roadObjects)
            {
                Destroy(item);
            }
            roadObjects.Clear();
        }
    }
}
