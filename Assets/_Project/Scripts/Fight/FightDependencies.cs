using _Project.Scripts.Messages;

namespace _Project.Scripts.Fight
{
    public class FightDependencies
    {
        public NotificationSender NotificationSender { get; }
        
        public FightDependencies(NotificationSender notificationSender)
        {
            NotificationSender = notificationSender;
        }
    }
}