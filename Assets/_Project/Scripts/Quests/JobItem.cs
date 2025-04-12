using _Project.Scripts.Dialogues;
using _Project.Scripts.Messages;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Quests
{
    public abstract class JobItem : MonoBehaviour
    {
        protected LevelPlayerData LevelPlayerData { get; private set; }
        protected NotificationSender NotificationSender { get; private set; }
        
        public void Initialize(QuestDependencies questDependencies)
        {
            LevelPlayerData = questDependencies.LevelPlayerData;
            NotificationSender = questDependencies.NotificationSender;
        }
        
        public abstract bool CheckReadyToInteract();
        public abstract void Interact();
    }
}