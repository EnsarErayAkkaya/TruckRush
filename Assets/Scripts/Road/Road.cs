using System.Collections.Generic;
using UnityEngine;

namespace Project.Road
{
    public class Road : MonoBehaviour
    {
        private List<GameObject> roadObjects = new List<GameObject>();
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
