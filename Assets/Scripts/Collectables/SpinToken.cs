using Project.GameSystems;
using System;
using UnityEngine;
using Project.Road;
using Project.UI;

namespace Project.Collectables
{
    public class SpinToken : Collectable
    {
        [SerializeField] private GameObject spinTokenUIPrefab;
        [SerializeField] private float speed;
        [SerializeField] private float radiusSpeed;
        [SerializeField] private float startingForwardOffset;
        [SerializeField] private float startingRightOffset;

        private Vector3 center;
        private Vector3 currentPos;
        private float width;
        private float currentWidth;
        private float widthIncrementer;
        float angle = 0;
        private void Start()
        {
            center = transform.position;
            width = ProceduralRoadGenerator.instance.Setting.roadWidthHalf;
            currentPos.y = center.y;
            radiusSpeed = UnityEngine.Random.Range(1, radiusSpeed);
        }

        private void Update()
        {
            angle += (Time.deltaTime * speed) % 360;
            
            widthIncrementer += Time.deltaTime * radiusSpeed;

            currentWidth = Mathf.PingPong(widthIncrementer, width);

            currentPos.x = center.x + currentWidth * Mathf.Sin(angle * Mathf.Deg2Rad);
            currentPos.z = center.z + currentWidth * Mathf.Cos(angle * Mathf.Deg2Rad);

            transform.position = currentPos;
        }
        public override int OnPlayerCollided()
        {
            PlayCollectSound();
            ScoreManager.instance.AddSpinToken();
            Notification.instance.AddNotification(spinTokenUIPrefab, transform.position);
            Destroy(gameObject);
            return -1;
        }
    }
}
