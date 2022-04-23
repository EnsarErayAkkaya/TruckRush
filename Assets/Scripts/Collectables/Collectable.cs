using Project.GameSystems;
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
        [SerializeField] protected string soundName;
        protected bool destroyed;

        public virtual int OnPlayerCollided()
        {
            PlayCollectSound();
            if (!destroyed)
            {
                destroyed = true;
                return value;
            }
            return -1;
        }
        protected void PlayCollectSound()
        {
            AudioManager.instance.Play(soundName);
        }
    }
}
