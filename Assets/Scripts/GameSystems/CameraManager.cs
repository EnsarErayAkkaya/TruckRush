using UnityEngine;
using Cinemachine;

namespace Project.GameSystems
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera mainCam;
        void Start()
        {
            mainCam.Follow = TruckManager.instance.truck;
        }
    }
}