using System;
using System.Collections;
using _Project.Scripts.Messages;
using _Project.Scripts.Other;
using UnityEngine;

namespace _Project.Scripts.Weed
{
    public abstract class Weed
    {
        public event Action<int> CountChanged;
        
        private int _count = 3;
        
        public WeedScriptableObject Data { get; protected set; }
        public float ActionTime { get; protected set; }
        public int MaxCount { get; } = 3;
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                CountChanged?.Invoke(_count);
            }
            
        }
        protected bool IsReadyToApplyEffect { get; set; } = true;
        protected CoroutineStarter CoroutineStarter { get; set; }
        protected NotificationSender NotificationSender { get; set; }

        
        public abstract void Initialize(WeedDependencies weedDependencies, WeedScriptableObject data);
        public abstract void ApplyEffect();
        protected abstract void RemoveEffect();
        
        protected IEnumerator TimerCoroutine()
        {
            yield return new WaitForSeconds(ActionTime);
            RemoveEffect();
        }
    }
}