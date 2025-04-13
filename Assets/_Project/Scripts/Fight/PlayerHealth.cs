using System;
using System.Collections;
using _Project.Scripts.Messages;
using _Project.Scripts.Other;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Fight
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _onDeathCameraPoint;
        [SerializeField] private GameObject _deathUI;
        [SerializeField] private Image _bloodOverlay; 
        [field: SerializeField] public float MaxHealth { get; private set; }
        [SerializeField] private AnimationCurve _bloodOverlayAlphaCurve;
        
        public event Action Died;

        private NotificationSender _notificationSender;
        private GameObject _hands;
        private BlackScreen _blackScreen;
        private Coroutine _regenerationCoroutine;
        private float _health;
        private bool _isDead;

        public float Health
        {
            get => _health;
            private set
            {
                _health = value;
                var color = _bloodOverlay.color;
                color.a = _bloodOverlayAlphaCurve.Evaluate(Health);
                _bloodOverlay.color = color;
            }
            
        }

        public void Initialize(FightDependencies fightDependencies, GameObject hands, BlackScreen blackScreen)
        {
            Health = MaxHealth;
            _notificationSender = fightDependencies.NotificationSender;
            _hands = hands;
            _blackScreen = blackScreen;
        }

        public void TakeDamage(float value)
        {
            if (Health <= 0 || value <= 0)
                return;

            if (_regenerationCoroutine != null)
                StopCoroutine(_regenerationCoroutine);
            
            
            Health -= value;
            NotifyAboutDamage();
            _regenerationCoroutine = StartCoroutine(RegenerationHealthCoroutine());
            
            if (Health < 0)
                Health = 0;

            if (Health <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            Died?.Invoke();
            _hands.SetActive(false);
            StartCoroutine(DieCoroutine());
        }

        private void NotifyAboutDamage()
        {
            if (Health <= MaxHealth / 3)
            {
                _notificationSender.Send("Ещё чу-чуть и я умру!");
                return;
            }
            
            int randomNumber = Random.Range(0, 10);
            string text = randomNumber switch
            {
                0 => "Это было больно!",
                1 => "Ауч!",
                2 => "Аргх...",
                3 => "",
                4 => "",
                5 => "",
                6 => "",
                7 => "",
                8 => "",
                9 => "",
                10 => "",
                _ => throw new InvalidOperationException()
            };

            if (text != "")
                _notificationSender.Send(text);
        }

        private IEnumerator DieCoroutine()
        {
            _camera.transform.DOMove(_onDeathCameraPoint.position, 1);
            _camera.transform.DORotateQuaternion(_onDeathCameraPoint.rotation, 1);
            _blackScreen.Enable();
            yield return new WaitForSeconds(_blackScreen.AnimationTime);
            _deathUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        private IEnumerator RegenerationHealthCoroutine()
        {
            yield return new WaitForSeconds(10);
            _notificationSender.Send("Мне становится лучше");
            
            while (Health < MaxHealth && !_isDead)
            {
                yield return new WaitForSeconds(1);
                Health++;
            }
        }
    }
}