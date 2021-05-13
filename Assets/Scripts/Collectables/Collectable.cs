using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Collectables
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] protected int value;
        private bool destroyed;

        public virtual int OnPlayerCollided()
        {
            if (!destroyed)
            {
                destroyed = true;
                return value;
            }
            return -1;
        }
    }
}
