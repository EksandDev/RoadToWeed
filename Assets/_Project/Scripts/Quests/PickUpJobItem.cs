using UnityEngine;

namespace _Project.Scripts.Quests
{
    [RequireComponent(typeof(Collider))]
    public class PickUpJobItem : JobItem
    {
        public override bool CheckReadyToInteract()
        {
            if (!LevelPlayerData.IsWorker)
                return false;

            return !LevelPlayerData.IsDoingJob;
        }

        public override void Interact()
        {
            if (!LevelPlayerData.IsWorker)
            {
                NotificationSender.Send("Ты не работник.");
                return;
            }

            if (LevelPlayerData.IsDoingJob)
            {
                NotificationSender.Send("Ты уже взял ящик.");
                return;
            }
            
            LevelPlayerData.IsDoingJob = true;
            NotificationSender.Send("Ты взял ящик.");
        }
    }
}