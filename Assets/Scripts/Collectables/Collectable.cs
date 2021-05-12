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
        [SerializeField] private int value;
        [SerializeField] private Animator animator;
        [SerializeField] private string collisionAnimationName;

        private bool destroyed;

        public virtual float OnPlayerCollided()
        {
            if (!destroyed)
            {
                destroyed = true;
                PlayCollisionAnimation();
                return value;
            }
            return -1;
        }
        protected void PlayCollisionAnimation()
        {
            animator.SetTrigger(collisionAnimationName);
        }
    }
}
