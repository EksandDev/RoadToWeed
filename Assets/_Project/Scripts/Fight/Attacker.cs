using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Fight
{
    public abstract class Attacker : MonoBehaviour
    {
        [SerializeField] private float _damage;

        private float _usualDamage;
        
        protected bool ReadyToAttack { get; set; } = true;

        protected abstract void Attack(IDamageable target, bool isStrong = false);
        protected abstract IEnumerator TimerCoroutine();

        private void Awake()
        {
            _usualDamage = _damage;
        }

        protected void DealDamage(IDamageable target, bool isStrong = false)
        {
            if (isStrong)
            {
                target.TakeDamage(_damage * 3);
                return;
            }
                
            target.TakeDamage(_damage);
        }
    }
}