using System;
using UnityEngine;

namespace _Project.Scripts.Quests
{
    [RequireComponent(typeof(Collider))]
    public class DropJobItem : JobItem
    {
        [SerializeField] private int _itemsAmountToEnd = 5;

        private int _itemsAmount;
        
        public event Action JobIsDone;
        
        public override bool CheckReadyToInteract()
        {
            if (LevelPlayerData.IsWorker && LevelPlayerData.IsDoingJob)
                return true;

            return false;
        }

        public override void Interact()
        {
            if (!LevelPlayerData.IsWorker)
            {
                NotificationSender.Send("Ты не работник.");
                return;
            }

            if (!LevelPlayerData.IsDoingJob)
            {
                NotificationSender.Send("Ты не взял ящик.");
                return; 
            }
            
            _itemsAmount++;
            LevelPlayerData.IsDoingJob = false;

            if (_itemsAmount >= _itemsAmountToEnd)
            {
                _itemsAmount = 0;
                JobIsDone?.Invoke();
                NotificationSender.Send($"Всё готово, возвращайся за деньгами.");
                return;
            }
                
            NotificationSender.Send($"Ты положил ящик. Осталось: {_itemsAmountToEnd - _itemsAmount}");
        }
    }
}