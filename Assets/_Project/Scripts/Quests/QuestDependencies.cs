using _Project.Scripts.Messages;
using _Project.Scripts.Player;

namespace _Project.Scripts.Quests
{
    public class QuestDependencies
    {
        public NotificationSender NotificationSender { get; }
        public LevelPlayerData LevelPlayerData { get; }

        public QuestDependencies(NotificationSender notificationSender, LevelPlayerData levelPlayerData)
        {
            NotificationSender = notificationSender;
            LevelPlayerData = levelPlayerData;
        }
    }
}