using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] protected float damage;
        [SerializeField] protected Animator animator;
        [SerializeField] protected string collisionAnimationName;

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
        protected virtual void PlayCollisionAnimation()
        {
            if(animator != null)
                animator.SetTrigger(collisionAnimationName);
        }
    }
}