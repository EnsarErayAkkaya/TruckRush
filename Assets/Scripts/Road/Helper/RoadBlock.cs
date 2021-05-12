using System;

namespace Project.Road
{
    [Serializable]
    public class RoadBlock
    {
        public float startPoint;
        public float endPoint;
        public RoadBlock(float startPoint, float endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }
        public bool IsValueInRange(float value)
        {
            return value >= startPoint && value <= endPoint;
        }
        public override string ToString()
        {
            return "StartingPoint: " + startPoint + ", endingPoint: " + endPoint;
        }
    }
}
