using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UI
{
    public class Notification : MonoBehaviour
    {
        #region singleton
        public static Notification instance;
        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("More then one instance of inventory found");
                return;
            }
            instance = this;
        }
        #endregion
        public List<string> notifications;
        public GameObject prefab;
        public Transform notificationsParent;
        private bool notificationRoutineStarted;
        [SerializeField] float notificationScreenDuration;
        [SerializeField] float collectableNotificationScreenDuration;
        [SerializeField] Camera cam;

        private void Start()
        {
            notifications = new List<string>();
        }
        public void AddNotification(string notification)
        {
            notifications.Add(notification);
            if (!notificationRoutineStarted)
                StartCoroutine(EnumerateNotifications());
        }
        /// <summary>
        /// Create UI Notification with given prefab at given position. Destroy after default time. Doesn't Effect main notification loop.
        /// </summary>
        /// <param name="notificationPrefab"></param>
        /// <param name="screenPos"></param>
        public void AddNotification(GameObject notificationPrefab, Vector3 worldPos)
        {
            Vector2 screenPos =  cam.WorldToScreenPoint(worldPos);
            GameObject g = Instantiate(notificationPrefab, notificationsParent);
            g.transform.position = screenPos;
            StartCoroutine(EnumerateCollectableNotification(g));
        }
        private IEnumerator EnumerateCollectableNotification(GameObject g)
        {
            CanvasGroup cg = g.GetComponent<CanvasGroup>();
            yield return CanvasGroupUtility.ChangeAlpha(cg, 0, 1);
            yield return new WaitForSeconds(collectableNotificationScreenDuration - 1);
            yield return CanvasGroupUtility.ChangeAlpha(cg, 1, 0);
        }

        private IEnumerator EnumerateNotifications()
        {
            notificationRoutineStarted = true;
            BaseNotificationUIObject csn = Instantiate(prefab, notificationsParent).GetComponent<BaseNotificationUIObject>();
            while (notifications.Count > 0)
            {
                csn.text.text = notifications[0];
                float t = notificationScreenDuration;
                while (t > 0)
                {
                    t -= Time.deltaTime;
                    csn.SetNotificationColorAlpha(t);
                    yield return null;
                }
                notifications.RemoveAt(0);
            }
            notificationRoutineStarted = false;
            Destroy(csn.gameObject);
        }
    }
}