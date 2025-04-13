using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Fight
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class Attacker : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private AudioClip _hitSound;

        private float _usualDamage;

        protected AudioSource AudioSource { get; set; }
        protected bool ReadyToAttack { get; set; } = true;

        protected abstract void Attack(IDamageable target, bool isStrong = false);
        protected abstract IEnumerator TimerCoroutine();

        private void Awake()
        {
            _usualDamage = _damage;
            AudioSource = GetComponent<AudioSource>();
        }

        protected void DealDamage(IDamageable target, bool isStrong = false)
        {
            AudioSource.PlayOneShot(_hitSound);
                
            if (isStrong)
            {
                target.TakeDamage(_damage * 3);
                return;
            }
                
            target.TakeDamage(_damage);
        }
    }
}