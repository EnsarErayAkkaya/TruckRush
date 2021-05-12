using System;
using System.Collections.Generic;

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

        private List<RoadBlock> roadAllocations;

        public RoadSegmentation()
        {
            roadAllocations = new List<RoadBlock>();
        }
        public void Set(float startingPoint, float endingPoint)
        {
            this.startingPoint = startingPoint;
            this.endingPoint = endingPoint;
            roadAllocations.Clear();
        }
        /// <summary>
        /// Allocate first empty space with requested size
        /// </summary>
        /// <param name="requestedLength"></param>
        /// <returns></returns>
        public bool AllocateSpace(float requestedLength, ref RoadBlock block)
        {
            float currentPoint = startingPoint;
            if (roadAllocations.Count > 0)
            {
                for (int i = 0; i < roadAllocations.Count; i++)
                {
                    float spaceLength = roadAllocations[i].startPoint - currentPoint;
                    if (spaceLength >= requestedLength)
                    {
                        block = new RoadBlock(currentPoint, currentPoint + requestedLength);
                        roadAllocations.Insert(i, block);
                        return true;
                    }
                    currentPoint = roadAllocations[i].endPoint;
                }
            }
            if (currentPoint + requestedLength <= endingPoint)
            {
                block = new RoadBlock(currentPoint, currentPoint + requestedLength);
                roadAllocations.Add(block);
                return true;
            }
            return false;
        }
    }
}
