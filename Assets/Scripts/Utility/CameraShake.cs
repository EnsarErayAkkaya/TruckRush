using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
namespace Project.Utility
{
    public class CameraShake : MonoBehaviour
    {
        private CinemachineVirtualCamera cam;
        private void Start()
        {
            cam = GetComponent<CinemachineVirtualCamera>();
        }
        public void Shake(float duration, float intensity)
        {
            StartCoroutine(ShakeRoutine(duration, intensity));
        }
        private IEnumerator ShakeRoutine(float duration, float intensity)
        {
            CinemachineBasicMultiChannelPerlin cbmcp = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cbmcp.m_AmplitudeGain = intensity;    
            yield return duration;
            cbmcp.m_AmplitudeGain = 0;
        }
    }
}