using System;
using _Project.Scripts.Fight.Enemies;
using _Project.Scripts.Messages;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Quests
{
    public class EnemiesController : QuestController
    {
        [SerializeField] private EnemyHealth[] _enemies;
        [SerializeField] private GameObject[] _objectsWillHide;

        private int _diedEnemies;
        private NotificationSender _notificationSender;
        private LevelPlayerData _levelPlayerData;
        
        public override void Initialize(NotificationSender notificationSender, LevelPlayerData levelPlayerData)
        {
            _notificationSender = notificationSender;
            _levelPlayerData = levelPlayerData;

            foreach (var item in _enemies)
                item.Died += OnEnemyDie;
        }

        private void OnEnemyDie()
        {
            _diedEnemies++;

            if (_diedEnemies == _enemies.Length)
            {
                _levelPlayerData.ReadyToLeave = true;
                _notificationSender.Send($"Отбросы раскиданы, можно уезжать");

                foreach (var item in _objectsWillHide)
                    item.SetActive(false);
                
                return;
            }
            
            _notificationSender.Send($"Отброс в отключке. Осталось ещё: {(_enemies.Length) - _diedEnemies}");
        }
    }
}