using Project.GameSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Collectables
{
    public class Coin : Collectable
    {
        [SerializeField] private float rotateSpeed;
        private void Update()
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }
    }
}
