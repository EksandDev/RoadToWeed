using _Project.Scripts.Messages;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Dialogues
{
    public class BuyCanisterDialogueObject : DialogueAndAnswerObject
    {
        [SerializeField] private DialogueWithAnswerScriptableObject _canisterQuestionDialogue;
        [SerializeField] private DialogueWithoutAnswerScriptableObject _afterPurchaseDialogue;
        [SerializeField] private GameObject _canisterOnFloor;
        [SerializeField] private GameObject _canisterInHand;

        private DialogueUI _dialogueUI;
        private NotificationSender _notificationSender;
        private Wallet _wallet;
        private bool _afterPurchase;

        public override void Initialize(DialogueDependencies dialogueDependencies)
        {
            _dialogueUI = dialogueDependencies.DialogueUI;
            _notificationSender = dialogueDependencies.NotificationSender;
            _wallet = dialogueDependencies.Wallet;
        }

        public override void StartDialogue()
        {
            if (_afterPurchase)
            {
                _dialogueUI.ShowDialogue(_afterPurchaseDialogue.Text);
                return;
            }

            _dialogueUI.ShowDialogue(_canisterQuestionDialogue.Text, _canisterQuestionDialogue.PlayerAnswers, this);
        }

        public override void ReceivePlayerAnswer(int buttonIndex)
        {
            switch (buttonIndex)
            {
                case 0:
                    _dialogueUI.HideDialogue();
                    _dialogueUI.HideAnswers();

                    if (_wallet.Money < 30)
                    {
                        _notificationSender.Send("Не хватает средств на покупку");
                        _dialogueUI.HideDialogue();
                        _dialogueUI.HideAnswers();
                        return;
                    }

                    _wallet.Money -= 30;
                    _afterPurchase = true;
                    _dialogueUI.ShowDialogue(_afterPurchaseDialogue.Text);
                    _canisterInHand.SetActive(false);
                    _canisterOnFloor.SetActive(true);
                    _notificationSender.Send("Канистра куплена");
                    return;
                case 1:
                    _dialogueUI.HideDialogue();
                    _dialogueUI.HideAnswers();
                    return;
            }
        }
    }
}