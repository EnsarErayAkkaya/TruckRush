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
        private new Transform camera;
        private void Start()
        {
            camera = Camera.main.transform;
        }
        private void LateUpdate()
        {
            transform.LookAt(camera);
            transform.Rotate(0, 180, 0);
        }
    }
}
