using System;
using _Project.Scripts.Messages;
using _Project.Scripts.Player;
using _Project.Scripts.Quests;
using UnityEngine;

namespace _Project.Scripts.Dialogues
{
    public class DialogueObjectWithJobQuest : DialogueAndAnswerObject
    {
        [SerializeField] private DialogueWithAnswerScriptableObject _jobQuestionDialogue;
        [SerializeField] private DialogueWithoutAnswerScriptableObject _waitJobIsDoneDialogue;
        [SerializeField] private DialogueWithoutAnswerScriptableObject _jobIsDoneDialogue;
        [SerializeField] private DialogueWithoutAnswerScriptableObject _playerReceiveJobDialogue;
        [SerializeField] private DropJobItem _dropJobItem;
        
        private DialogueUI _dialogueUI;
        private NotificationSender _notificationSender;
        private LevelPlayerData _levelPlayerData;
        private Wallet _wallet;
        private bool _jobIsDone;
        
        public override void Initialize(DialogueDependencies dialogueDependencies)
        {
            _dialogueUI = dialogueDependencies.DialogueUI;
            _notificationSender = dialogueDependencies.NotificationSender;
            _levelPlayerData = dialogueDependencies.LevelPlayerData;
            _wallet = dialogueDependencies.Wallet;
            _dropJobItem.JobIsDone += OnJobIsDone;
        }

        public override void StartDialogue()
        {
            switch (_jobIsDone)
            {
                case false when !_levelPlayerData.IsWorker:
                    _dialogueUI.ShowDialogue(_jobQuestionDialogue.Text, _jobQuestionDialogue.PlayerAnswers, this);
                    return;
                case false when _levelPlayerData.IsWorker:
                    _dialogueUI.ShowDialogue(_waitJobIsDoneDialogue.Text);
                    return;
                case true when !_levelPlayerData.IsWorker:
                    _dialogueUI.ShowDialogue(_jobIsDoneDialogue.Text);
                    _jobIsDone = false;
                    _wallet.Money += 20;
                    _notificationSender.Send("Ты получил деньги");
                    return;
                default:
                    throw new InvalidOperationException();
            }
        }

        public override void ReceivePlayerAnswer(int buttonIndex)
        {
            switch (buttonIndex)
            {
                case 0:
                    _dialogueUI.HideDialogue();
                    _dialogueUI.HideAnswers();
                    _dialogueUI.ShowDialogue(_playerReceiveJobDialogue.Text);
                    _levelPlayerData.IsWorker = true;
                    _notificationSender.Send("Ты принят на работу");
                    return;
                case 1:
                    _dialogueUI.HideDialogue();
                    _dialogueUI.HideAnswers();
                    return;
            }
        }

        private void OnJobIsDone()
        {
            _jobIsDone = true;
            _levelPlayerData.IsWorker = false;
        }
    }
}