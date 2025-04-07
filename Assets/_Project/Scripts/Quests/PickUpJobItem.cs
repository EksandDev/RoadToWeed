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
            LevelPlayerData.IsDoingJob = true;
        }
    }
}