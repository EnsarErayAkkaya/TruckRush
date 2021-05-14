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
        [SerializeField] float cardSetNotificationScreenDuration;

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

        private IEnumerator EnumerateNotifications()
        {
            notificationRoutineStarted = true;
            BaseNotificationUIObject csn = Instantiate(prefab, notificationsParent).GetComponent<BaseNotificationUIObject>();
            while (notifications.Count > 0)
            {
                csn.text.text = notifications[0];
                float t = cardSetNotificationScreenDuration;
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