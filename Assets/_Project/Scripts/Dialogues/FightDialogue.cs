using _Project.Scripts.Fight.Enemies;
using _Project.Scripts.Messages;
using UnityEngine;

namespace _Project.Scripts.Dialogues
{
    public class FightDialogue : DialogueAndAnswerObject
    {
        [SerializeField] private DialogueWithAnswerScriptableObject _fightDialogue;
        [SerializeField] private EnemyAttacker _enemyAttacker;
        [SerializeField] private Transform _playerTransform;
        
        private DialogueUI _dialogueUI;
        private NotificationSender _notificationSender;
        
        public override void Initialize(DialogueDependencies dialogueDependencies)
        {
            _dialogueUI = dialogueDependencies.DialogueUI;
            _notificationSender = dialogueDependencies.NotificationSender;
        }

        public override void StartDialogue()
        {
            _dialogueUI.ShowDialogue(_fightDialogue.Text, _fightDialogue.PlayerAnswers, this);
        }

        public override void ReceivePlayerAnswer(int buttonIndex)
        {
            switch (buttonIndex)
            {
                case 0:
                    _dialogueUI.HideDialogue();
                    _dialogueUI.HideAnswers();
                    _enemyAttacker.Enable(_playerTransform);
                    _notificationSender.Send("Ты его разозлил");
                    return;
                case 1:
                    _dialogueUI.HideDialogue();
                    _dialogueUI.HideAnswers();
                    return;
            }
        }
    }
}