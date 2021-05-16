using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class TruckAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private List<GameObject> wings;
        [SerializeField] private List<GameObject> shields;

        public void OpenWings()
        {
            foreach (GameObject item in wings)
            {
                item.SetActive(true);
            }
        }
        public void CloseWings()
        {
            foreach (GameObject item in wings)
            {
                item.SetActive(false);
            }
        }
        public void OpenShields()
        {
            foreach (GameObject item in shields)
            {
                item.SetActive(true);
            }
        }
        public void CloseShields()
        {
            foreach (GameObject item in shields)
            {
                item.SetActive(false);
            }
        }
    }
}
