using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class TruckAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void OpenWings()
        {
            animator.SetTrigger("OpenWings");
        }
        public void CloseWings()
        {
            animator.SetTrigger("CloseWings");
        }
    }
}
