using Project.Interfaces;
using UnityEngine;

namespace Project.Obstacles
{
    public class Barricade : Obstacle, IFrangible
    {
        [SerializeField] private GameObject fraction;
        [SerializeField] private GameObject mesh;
        [SerializeField] private float explosionRadius;
        [SerializeField] private float explosionForce;
        [SerializeField] private LayerMask explosionLayerMask;

        public void BreakOff()
        {
            GameObject g = Instantiate(fraction, transform.position, transform.rotation);
            Destroy(g, 4);
            float randomPosZ = Random.Range(0f, 1f);
            float randomPosY = Random.Range(0f, 1f);

            Vector3 explosionPos = transform.position - transform.forward * randomPosZ + transform.up * randomPosY;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius, explosionLayerMask);
            foreach (Collider item in colliders)
            {
                item.attachedRigidbody.AddExplosionForce(explosionForce, explosionPos, explosionRadius);
            }
            Destroy(gameObject);
        }
    }
}
