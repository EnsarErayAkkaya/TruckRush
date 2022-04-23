using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class TruckWings : MonoBehaviour
    {
        [SerializeField] private MeshFilter[] meshFilters;
        [SerializeField] private MeshRenderer[] meshRenderers;

        public void SetWingMeshes(Mesh wing, Material material)
        {
            foreach (MeshFilter item in meshFilters)
            {
                item.sharedMesh = wing;
            }
            foreach (MeshRenderer item in meshRenderers)
            {
                item.sharedMaterial = material;
            }
        }
    }
}