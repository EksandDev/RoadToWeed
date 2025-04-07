using _Project.Scripts.Player;

namespace _Project.Scripts.Dialogues
{
    public class DialogueDependencies
    {
        public DialogueUI DialogueUI { get; }
        public LevelPlayerData LevelPlayerData { get; }
        public Wallet Wallet { get; }
        
        public DialogueDependencies(DialogueUI dialogueUI, LevelPlayerData levelPlayerData, Wallet wallet)
        {
            DialogueUI = dialogueUI;
            LevelPlayerData = levelPlayerData;
            Wallet = wallet;
        }
    }
}