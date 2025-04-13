using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Fight.Enemies
{
    public class EnemyAttacker : Attacker
    {
        [SerializeField] private Transform _player;
        [SerializeField] private EnemyMover _enemyMover;
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _cooldown = 1f;
        [SerializeField] private float _timeToAttack = 1.04f;
        [SerializeField] private bool _enableOnStart;

        private bool _isReadyToAttack = true;
        private bool _isEnabled;
        private bool _targetInTrigger;
        private bool _isDied;
        private IDamageable _target;
        
        private const string _isAttackingAnimator = "IsAttacking";
        private const string _isFightingAnimator = "IsFighting";

        private void Start()
        {
            if (_enableOnStart)
            {
                Enable(_player);
                _enemyHealth.IsEnabled = true;
            }
            
            _enemyHealth.Died += () => _isDied = true;
        }

        public void Enable(Transform target)
        {
            _isEnabled = true;
            _enemyMover.Target = target;
            _enemyHealth.IsEnabled = true;
            _animator.SetBool(_isFightingAnimator, true);
        }

        public void Disable()
        {
            _isEnabled = false;
            _enemyMover.Target = null;
            _animator.SetBool(_isFightingAnimator, false);
        }
        
        protected override void Attack(IDamageable target, bool isStrong = false)
        {
            if (!_isReadyToAttack || target == null)
                return;
            
            _isReadyToAttack = false;
            _target = target;
            _enemyMover.Agent.isStopped = true;
            _animator.SetBool(_isAttackingAnimator, true);
            StartCoroutine(TimerCoroutine());
        }

        protected override IEnumerator TimerCoroutine()
        {
            yield return new WaitForSeconds(_timeToAttack);

            if (_isDied)
                yield break;
            
            if (_targetInTrigger)
                DealDamage(_target);
            
            _target = null;
            _enemyMover.Agent.isStopped = false;
            _animator.SetBool(_isAttackingAnimator, false);
            yield return new WaitForSeconds(_cooldown);
            _isReadyToAttack = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_isEnabled || _isDied || !other.TryGetComponent(out PlayerHealth playerHealth))
                return;
            
            Attack(playerHealth);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isEnabled || _isDied || !other.TryGetComponent(out PlayerHealth playerHealth))
                return;
            
            if (!_targetInTrigger)
                _targetInTrigger = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_isEnabled || _isDied || !other.TryGetComponent(out PlayerHealth playerHealth))
                return;
            
            if (_targetInTrigger)
                _targetInTrigger = false;
        }
    }
}