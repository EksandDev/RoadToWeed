using System.Collections;
using _Project.Scripts.Messages;
using _Project.Scripts.Other;
using UnityEngine;

namespace _Project.Scripts.Weed
{
    public abstract class Weed
    {
        protected bool IsReadyToApplyEffect { get; set; } = true;
        protected float ActionTime { get; set; }
        protected CoroutineStarter CoroutineStarter { get; set; }
        protected NotificationSender NotificationSender { get; set; }
        
        public abstract void Initialize(WeedDependencies weedDependencies);
        public abstract void ApplyEffect();
        protected abstract void RemoveEffect();
        
        protected IEnumerator TimerCoroutine()
        {
            yield return new WaitForSeconds(ActionTime);
            RemoveEffect();
        }
    }
}