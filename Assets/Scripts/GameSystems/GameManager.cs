using UnityEngine;

namespace Project.GameSystems
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        private void Awake()
        {
            instance = this;
        }
        public void OnPlayerFailure()
        {
            Debug.Log("Failure");
        }
    }
}
