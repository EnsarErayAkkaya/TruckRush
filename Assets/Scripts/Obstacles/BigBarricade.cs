using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Obstacles
{
    public class BigBarricade : Obstacle
    {
        [SerializeField] private GameObject left;
        [SerializeField] private GameObject right;
        public void Set(int side)
        {
            if (side >= 0)
                left.SetActive(false);
            else
                right.SetActive(false);
        }
    }
}