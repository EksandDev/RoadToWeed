using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Fight
{
    public class PlayerAttacker : Attacker
    {
        [SerializeField] private float _distance;
        [SerializeField] private float _cooldown;
        [SerializeField] private float _timeToAttack;
        [SerializeField] private float _timeToChargeStrongAttack;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private Camera _camera;
        [SerializeField] private Animator _handsAnimator;
        [SerializeField] private ParticleSystem _fireParticleSystem;
        [SerializeField] private AudioClip _strongAttackIsChargedSound;
        [SerializeField] private AudioClip _fistWhooshSound;

        private AudioSource _audioSource;
        private RaycastHit _hit;
        private Coroutine _chargeCoroutine;
        private bool _strongAttackIsReady;
        
        private const string _isAttackingAnimator = "IsAttacking";
        private const string _isRightAttackAnimator = "IsRightAttack";
        private const string _isLeftAttackAnimator = "IsLeftAttack";
        private const string _isChargingAnimator = "IsCharging";
        private const string _isStrongAttackingAnimator = "IsStrongAttacking";

        public bool IsEnabled { get; set; } = true;
        public bool CanStrongAttack { get; set; } = true;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _playerHealth.Died += () => IsEnabled = false;
        }

        private void Update()
        {
            if (!IsEnabled)
                return;
            
            TryStrongAttack();
            TryAttack();
        }

        protected override void Attack(IDamageable target, bool isStrong = false)
        {
            DealDamage(target, isStrong);
            StartCoroutine(TimerCoroutine());
        }
        
        protected override IEnumerator TimerCoroutine()
        {
            yield return new WaitForSeconds(_cooldown);
            ReadyToAttack = true;
        }

        private void TryAttack()
        {
            if (!ReadyToAttack || !Input.GetMouseButtonDown(0))
                return;
            
            _audioSource.PlayOneShot(_fistWhooshSound);
            StartCoroutine(AttackCoroutine());
        }

        private void TryStrongAttack()
        {
            if (Input.GetMouseButtonUp(1))
            {
                if (_handsAnimator.GetBool(_isChargingAnimator))
                    _handsAnimator.SetBool(_isChargingAnimator, false);

                if (!CanStrongAttack)
                {
                    _strongAttackIsReady = false;
                    _fireParticleSystem.Stop();
                    return;
                }
                
                if (_strongAttackIsReady)
                {
                    _audioSource.PlayOneShot(_fistWhooshSound);
                    StartCoroutine(StrongAttackCoroutine());
                    return;
                }

                if (!ReadyToAttack)
                    ReadyToAttack = true;
                
                if (_chargeCoroutine != null)
                    StopCoroutine(_chargeCoroutine);
            }

            if (!Input.GetMouseButtonDown(1) || !CanStrongAttack)
                return;

            if (!ReadyToAttack)
                return;
            
            if (!_handsAnimator.GetBool(_isChargingAnimator))
                _handsAnimator.SetBool(_isChargingAnimator, true);
            
            _chargeCoroutine = StartCoroutine(ChargeCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            ReadyToAttack = false;
            _handsAnimator.SetBool(_isAttackingAnimator, true);
            ChangeHandsAttackAnimation();
            yield return new WaitForSeconds(_timeToAttack);
            _handsAnimator.SetBool(_isAttackingAnimator, false);

            if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, _distance, ~6)
                || !_hit.transform.TryGetComponent(out IDamageable target))
            {
                ReadyToAttack = true;
                yield break;
            }
            
            Attack(target);
        }

        private IEnumerator ChargeCoroutine()
        {
            ReadyToAttack = false;
            yield return new WaitForSeconds(_timeToChargeStrongAttack);
            _strongAttackIsReady = true;
            _chargeCoroutine = null;
            _fireParticleSystem.Play();
            AudioSource.PlayOneShot(_strongAttackIsChargedSound);
        }

        private IEnumerator StrongAttackCoroutine()
        {
            _strongAttackIsReady = false;
            _handsAnimator.SetBool(_isStrongAttackingAnimator, true);
            yield return new WaitForSeconds(_timeToAttack);
            _handsAnimator.SetBool(_isStrongAttackingAnimator, false);
            _fireParticleSystem.Stop();

            if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, _distance, ~6)
                || !_hit.transform.TryGetComponent(out IDamageable target))
            {
                ReadyToAttack = true;
                yield break;
            }
            
            Attack(target, true);
        }

        private void ChangeHandsAttackAnimation()
        {
            var isRightAttack = _handsAnimator.GetBool(_isRightAttackAnimator);
            var isLeftAttack = _handsAnimator.GetBool(_isLeftAttackAnimator);
            
            switch (isRightAttack)
            {
                case false when !isLeftAttack:
                    _handsAnimator.SetBool(_isRightAttackAnimator, true);
                    return;
                case true:
                    _handsAnimator.SetBool(_isRightAttackAnimator, false);
                    _handsAnimator.SetBool(_isLeftAttackAnimator, true);
                    return;
                default:
                    _handsAnimator.SetBool(_isRightAttackAnimator, true);
                    _handsAnimator.SetBool(_isLeftAttackAnimator, false);
                    break;
            }
        }
    }
}