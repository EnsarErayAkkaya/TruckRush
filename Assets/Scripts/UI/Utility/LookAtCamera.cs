using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.UI
{
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private bool dontRotate180;
        private Transform cam;
        private int extraAngle;
        private void Start()
        {
            cam = Camera.main.transform;
            extraAngle = dontRotate180 ? 0 : 180;
        }
        private void LateUpdate()
        {
            transform.LookAt(cam);
            transform.Rotate(0, extraAngle, 0);
        }
    }
}
