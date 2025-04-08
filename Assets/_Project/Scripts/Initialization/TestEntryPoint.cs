using _Project.Scripts.Dialogues;
using _Project.Scripts.Fight;
using _Project.Scripts.Messages;
using _Project.Scripts.Player;
using _Project.Scripts.Quests;
using UnityEngine;

namespace _Project.Scripts.Initialization
{
    public class TestEntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private DialogueUI _dialogueUI;
        [SerializeField] private NotificationSender _notificationSender;
        [SerializeField] private DialogueObject[] _dialogueObjects;
        [SerializeField] private JobItem[] _jobItems;
        
        private void Start()
        {
            LevelPlayerData levelPlayerData = new();
            _dialogueUI.Initialize(new());
            Wallet wallet = new();
            DialogueDependencies dialogueDependencies = new(_dialogueUI, _notificationSender, levelPlayerData, wallet);
            QuestDependencies questDependencies = new(_notificationSender, levelPlayerData);
            FightDependencies fightDependencies = new(_notificationSender);
            _playerHealth.Initialize(fightDependencies);

            foreach (var jobItem in _jobItems)
                jobItem.Initialize(questDependencies);
            
            foreach (var dialogueObject in _dialogueObjects)
                dialogueObject.Initialize(dialogueDependencies);
        }
    }
}