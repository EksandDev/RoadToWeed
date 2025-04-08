using System;
using _Project.Scripts.Messages;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Fight
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public float MaxHealth { get; private set; }

        private NotificationSender _notificationSender;
        
        public float Health { get; private set; }

        public void Initialize(FightDependencies fightDependencies)
        {
            _notificationSender = fightDependencies.NotificationSender;
            Health = MaxHealth;
        }

        public void TakeDamage(float value)
        {
            if (Health <= 0 || value <= 0)
                return;

            Health -= value;
            NotifyAboutDamage();
            
            if (Health < 0)
                Health = 0;

            if (Health <= 0)
                Die();
        }

        private void Die()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    }
}