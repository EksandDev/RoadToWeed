using _Project.Scripts.Messages;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Quests
{
    public abstract class QuestController : MonoBehaviour
    {
        public abstract void Initialize(NotificationSender notificationSender, LevelPlayerData levelPlayerData);
    }
}