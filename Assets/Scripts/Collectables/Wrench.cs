using Project.GameSystems;
using UnityEngine;

namespace Project.Collectables
{
    public class Wrench : Collectable
    {
        [SerializeField] private float rotateSpeed;
        private void Update()
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
        public override int OnPlayerCollided()
        {
            PlayCollectSound();
            RepairManager.instance.Add();
            Destroy(gameObject);
            return -1;
        }
    }
}