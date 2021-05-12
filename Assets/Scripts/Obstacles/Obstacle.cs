using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private Animator animator;
        [SerializeField] private string collisionAnimationName;

        private bool destroyed;

        public virtual float OnPlayerCollided()
        {
            if(!destroyed)
            {
                destroyed = true;
                PlayCollisionAnimation();
                return damage;
            }
            return -1;
        }
        protected void PlayCollisionAnimation()
        {
            animator.SetTrigger(collisionAnimationName);
        }
    }
}