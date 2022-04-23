using Project.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.OffRoadObject
{
    public class CrosswalkSign : MonoBehaviour, IFrangible
    {
        [SerializeField] private GameObject fraction;
        [SerializeField] private float explosionRadius;
        [SerializeField] private float explosionForce;
        [SerializeField] private LayerMask explosionLayerMask;

        public void BreakOff()
        {
            GameObject g = Instantiate(fraction, transform.position, Quaternion.Euler(0, transform.rotation.y + 180, 0));
            Destroy(g, 4);
            float randomPosZ = Random.Range(0f, 1f);
            float randomPosY = Random.Range(0f, 4f);

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