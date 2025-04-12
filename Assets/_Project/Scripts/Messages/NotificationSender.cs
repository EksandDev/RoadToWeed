using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Messages
{
    public class NotificationSender : MonoBehaviour
    {
        [SerializeField] private int _maxNotifications = 5;
        [SerializeField] private float _notificationsLifeDuration = 3;
        [SerializeField] private Notification _notificationPrefab;

        private Queue<Notification> _overMaxNotificationsQueue = new();
        private int _notificationsAmount;
        
        public void Send(string message)
        {
            var newNotification = Instantiate(_notificationPrefab, transform);
            newNotification.Message = message;
            
            if (_notificationsAmount >= _maxNotifications)
            {
                newNotification.gameObject.SetActive(false);
                _overMaxNotificationsQueue.Enqueue(newNotification);
                return;
            }

            if (_overMaxNotificationsQueue.Count != 0)
            {
                var waitingNotification = _overMaxNotificationsQueue.Dequeue();
                InitializeNotification(waitingNotification);
                waitingNotification.gameObject.SetActive(true);
                return;
            }
            
            InitializeNotification(newNotification);
        }

        private void InitializeNotification(Notification notification)
        {
            notification.Initialize();
            _notificationsAmount++;
            StartCoroutine(DestroyNotificationCoroutine(notification.gameObject));
        }

        private IEnumerator DestroyNotificationCoroutine(GameObject notification)
        {
            yield return new WaitForSeconds(_notificationsLifeDuration);
            _notificationsAmount--;
            Destroy(notification);

            if (_overMaxNotificationsQueue.Count == 0) 
                yield break;
            
            var waitingNotification = _overMaxNotificationsQueue.Dequeue();
            waitingNotification.gameObject.SetActive(true);
            InitializeNotification(waitingNotification);
        }
    }
}