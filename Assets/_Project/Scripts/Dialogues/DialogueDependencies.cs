using _Project.Scripts.Messages;
using _Project.Scripts.Player;

namespace _Project.Scripts.Dialogues
{
    public class DialogueDependencies
    {
        public DialogueUI DialogueUI { get; }
        public NotificationSender NotificationSender { get; }
        public LevelPlayerData LevelPlayerData { get; }
        public Wallet Wallet { get; }
        
        public DialogueDependencies(DialogueUI dialogueUI, NotificationSender notificationSender, 
            LevelPlayerData levelPlayerData, Wallet wallet)
        {
            DialogueUI = dialogueUI;
            NotificationSender = notificationSender;
            LevelPlayerData = levelPlayerData;
            Wallet = wallet;
        }
    }
}