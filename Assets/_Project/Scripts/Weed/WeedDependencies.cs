using _Project.Scripts.Messages;
using _Project.Scripts.Other;
using _Project.Scripts.Player;

namespace _Project.Scripts.Weed
{
    public class WeedDependencies
    {
        public PlayerController PlayerController { get; }
        public CoroutineStarter CoroutineStarter { get; }
        public NotificationSender NotificationSender { get; }
        public HiddenItemsController HiddenItemsController { get; }

        public WeedDependencies(PlayerController playerController, CoroutineStarter coroutineStarter, 
            NotificationSender notificationSender, HiddenItemsController hiddenItemsController)
        {
            PlayerController = playerController;
            CoroutineStarter = coroutineStarter;
            NotificationSender = notificationSender;
            HiddenItemsController = hiddenItemsController;
        }
    }
}