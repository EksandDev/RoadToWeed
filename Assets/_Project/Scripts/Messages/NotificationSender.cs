using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Messages
{
    [RequireComponent(typeof(AudioSource))]
    public class NotificationSender : MonoBehaviour
    {
        [SerializeField] private int _maxNotifications = 5;
        [SerializeField] private float _notificationsLifeDuration = 3;
        [SerializeField] private Notification _notificationPrefab;
        [SerializeField] private AudioClip _notificationSound;

        private Queue<Notification> _overMaxNotificationsQueue = new();
        private AudioSource _audioSource;
        private int _notificationsAmount;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

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
            _audioSource.PlayOneShot(_notificationSound);
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