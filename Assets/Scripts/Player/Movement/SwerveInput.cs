using UnityEngine;

namespace Project.Player
{
    public class SwerveInput : MonoBehaviour
    {
        private Vector2 startingPoint;

        public Vector2 swerveVector;
        private void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
            if (Input.GetMouseButton(0))
            {
                swerveVector = ((Vector2)Input.mousePosition - startingPoint).normalized;
            }
            if (Input.GetMouseButtonDown(0))
            {
                startingPoint = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                swerveVector = Vector2.zero;
            }
#endif

#if UNITY_ANDROID
            if(Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Moved)
                {
                    swerveVector = -(startingPoint - (Vector2)Input.mousePosition).normalized;
                }
                if (t.phase == TouchPhase.Began)
                {
                    startingPoint = Input.mousePosition;
                }
                if (t.phase == TouchPhase.Ended)
                {
                    swerveVector = Vector2.zero;
                }
            }
#endif
        }
    }
}