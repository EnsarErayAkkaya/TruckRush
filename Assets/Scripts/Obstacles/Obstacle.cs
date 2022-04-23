using Project.GameSystems;
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
        [SerializeField] protected string soundName;

        protected bool destroyed;

        public virtual float OnPlayerCollided()
        {
            if(!destroyed)
            {
                PlayCrashSound();
                destroyed = true;
                PlayCollisionEffect();
                return damage;
            }
            return -1;
        }
        protected virtual void PlayCollisionEffect()
        {
            if(animator != null)
                animator.SetTrigger(collisionAnimationName);
        }
        protected void PlayCrashSound()
        {
            AudioManager.instance.Play(soundName);
        }
    }
}