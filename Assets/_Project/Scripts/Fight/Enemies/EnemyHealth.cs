using System;
using UnityEngine;

namespace _Project.Scripts.Fight.Enemies
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem _bloodVFX;
        [field: SerializeField] public float MaxHealth { get; private set; }

        private Collider _collider;
        private bool _isDead;
        
        public event Action PlayerDied;
        
        private const string _isDeadAnimator = "IsDead";
        
        public float Health { get; private set; }

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            Health = MaxHealth;
        }

        public void TakeDamage(float value)
        {
            if (_isDead || Health <= 0 || value <= 0)
                return;

            Health -= value;
            _bloodVFX.Play();
            
            if (Health < 0)
                Health = 0;

            if (Health <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _collider.enabled = false;
            _animator.SetBool(_isDeadAnimator, true);
            PlayerDied?.Invoke();
        }
    }
}