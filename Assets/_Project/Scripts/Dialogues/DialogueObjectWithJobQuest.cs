namespace _Project.Scripts.Dialogues
{
    public class DialogueObjectWithJobQuest : DialogueObject
    {
        private DialogueUI _dialogueUI;
        
        public override void Initialize(DialogueUI dialogueUI)
        {
            _dialogueUI = dialogueUI;
        }

        public override void StartDialogue()
        {
            // старт диалог, но с условием выполнения квеста
        }
    }
}