using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Road
{
    [Serializable]
    class RoadSegmentation
    {
        private float startingPoint;
        private float endingPoint;
        public float StartingPoint => startingPoint;
        public float EndingPoint => endingPoint;
        public float RoadLength => endingPoint - startingPoint;
        public float RemainingLength => endingPoint - currentPoint;

        private float currentPoint;

        public RoadSegmentation()
        {
            currentPoint = 0;
        }
        public void Set(float startingPoint, float endingPoint)
        {
            this.startingPoint = startingPoint;
            this.endingPoint = endingPoint;
            currentPoint = startingPoint;
        }
        /// <summary>
        /// Allocate first empty space with requested size
        /// </summary>
        /// <param name="requestedLength"></param>
        /// <returns></returns>
        public float AllocateSpace(float requestedLength)
        {
            if (currentPoint + requestedLength <= endingPoint)
            {
                currentPoint += requestedLength;
                return currentPoint;
            }
            return -1;
        }
    }
}
