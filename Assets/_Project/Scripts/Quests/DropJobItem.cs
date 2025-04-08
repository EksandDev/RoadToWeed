using System;
using UnityEngine;

namespace _Project.Scripts.Quests
{
    [RequireComponent(typeof(Collider))]
    public class DropJobItem : JobItem
    {
        public event Action JobIsDone;
        
        public override bool CheckReadyToInteract()
        {
            return LevelPlayerData.IsWorker && LevelPlayerData.IsDoingJob;
        }

        public override void Interact()
        {
            LevelPlayerData.IsDoingJob = false;
            JobIsDone?.Invoke();
            NotificationSender.Send("Ты положил ящик.");
        }
    }
}