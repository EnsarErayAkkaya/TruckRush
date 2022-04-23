using Project.GameSystems;
using Project.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.OffRoadObject
{
    public class StreetLight : MonoBehaviour, IFrangible
    {
        [SerializeField] private Light spotLigth;
        [SerializeField] private GameObject volumMesh;

        [SerializeField] private GameObject fraction;
        [SerializeField] private float explosionRadius;
        [SerializeField] private float explosionForce;
        [SerializeField] private LayerMask explosionLayerMask;

        [SerializeField] private MeshRenderer meshRenderer;

        [SerializeField] private float activateDist;
        [SerializeField] private float intensityChangeDuration;
        [SerializeField] private float targetIntensity;
        [SerializeField] private float targetAlpha;
        
        private Transform truck;
        private bool isEnabled;
        private bool isActive;
        private void Start()
        {
            truck = GameObject.FindGameObjectWithTag("Player").transform;
            DayNightManager.instance.onDay += CloseLight;
            DayNightManager.instance.onNight += OpenLight;
            isEnabled = DayNightManager.instance.IsDay;
            if (isEnabled)
                OpenLight();
        }
        private void OnDestroy()
        {
            DayNightManager.instance.onDay -= CloseLight;
            DayNightManager.instance.onNight -= OpenLight;
        }
        private void Update()
        {
            if(isEnabled)
            {
                if (Vector3.Distance(transform.position, truck.position) < activateDist && !isActive)
                {
                    isActive = true;
                    StartCoroutine(OpenLightEnumearete());
                }
            }
        }
        public void OpenLight()
        {
            isEnabled = true;
        }
        public void CloseLight()
        {
            isActive = false;
            isEnabled = false;
            StartCoroutine(CloseLightEnumearete());
        }
        private IEnumerator CloseLightEnumearete()
        {
            float t = 0;
            Color targetC = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, 0);
            while (t <= 1)
            {
                t += Time.deltaTime / intensityChangeDuration;
                spotLigth.intensity = Mathf.Lerp(spotLigth.intensity, 0, t);
                meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, targetC, t );

                if(meshRenderer.material.color.a < .1f && volumMesh.activeSelf)
                {
                    meshRenderer.material.color = targetC;
                    volumMesh.SetActive(false);
                }

                yield return new WaitForEndOfFrame();
            }
            spotLigth.enabled = false;
        }
        private IEnumerator OpenLightEnumearete()
        {
            volumMesh.SetActive(true);
            spotLigth.enabled = true;
            float t = 0;
            Color targetC = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, targetAlpha);
            while (t <= 1)
            {
                t += Time.deltaTime / intensityChangeDuration;
                spotLigth.intensity = Mathf.Lerp(spotLigth.intensity, targetIntensity, t);
                meshRenderer.material.color = Color.Lerp(meshRenderer.material.color, targetC, t);
                yield return new WaitForEndOfFrame();
            }
        }

        public void BreakOff()
        {
            GameObject g = Instantiate(fraction, transform.position, Quaternion.Euler(0, transform.rotation.y + 180, 0));
            Destroy(g, 4);
            float randomPosZ = Random.Range(0f, 1f);
            float randomPosY = Random.Range(4f, 6f);

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