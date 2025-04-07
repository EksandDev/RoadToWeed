using UnityEngine;

namespace _Project.Scripts.Dialogues
{
    public class DialogueObjectWithoutQuest : DialogueObject
    {
        [SerializeField] private DialogueWithoutAnswerScriptableObject _firstDialogue;
        [SerializeField] private DialogueWithoutAnswerScriptableObject _secondDialogue;
        
        private bool _firstDialogueIsOut;
        private DialogueUI _dialogueUI;

        public override void Initialize(DialogueUI dialogueUI)
        {
            _dialogueUI = dialogueUI;
        }

        public override void StartDialogue()
        {
            if (!_firstDialogueIsOut)
            {
                _dialogueUI.ShowDialogue(_firstDialogue.Text);
                _firstDialogueIsOut = true;
                return;
            }
              
            _dialogueUI.ShowDialogue(_secondDialogue.Text);
        }
    }
}