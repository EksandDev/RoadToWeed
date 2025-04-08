using System;
using System.Collections.Generic;
using _Project.Scripts.Dialogues;
using _Project.Scripts.Fight;
using _Project.Scripts.Messages;
using _Project.Scripts.Other;
using _Project.Scripts.Player;
using _Project.Scripts.Quests;
using _Project.Scripts.Weed;
using UnityEngine;

namespace _Project.Scripts.Initialization
{
    public class TestEntryPoint : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerHealth _playerHealth;
        
        [Header("Dialogues")]
        [SerializeField] private DialogueUI _dialogueUI;
        [SerializeField] private DialogueObject[] _dialogueObjects;
        
        [Header("Weed")]
        [SerializeField] private GameObject[] _hiddenItems;
        [SerializeField] private WeedInventorySlot[] _weedInventorySlots;
        [SerializeField] private WeedInventorySlotSelector _weedInventorySlotSelector;
        
        [Header("Other")]
        [SerializeField] private CoroutineStarter _coroutineStarter;
        [SerializeField] private NotificationSender _notificationSender;
        [SerializeField] private JobItem[] _jobItems;

        private List<Weed.Weed> _weeds;
        
        private void Start()
        {
            LevelPlayerData levelPlayerData = new();
            _dialogueUI.Initialize(new());
            Wallet wallet = new();
            HiddenItemsController hiddenItemsController = new(_hiddenItems);
            DialogueDependencies dialogueDependencies = new(_dialogueUI, _notificationSender, levelPlayerData, wallet);
            QuestDependencies questDependencies = new(_notificationSender, levelPlayerData);
            FightDependencies fightDependencies = new(_notificationSender);
            WeedDependencies weedDependencies = new(_playerController, _coroutineStarter, _notificationSender,
                hiddenItemsController);
            _playerHealth.Initialize(fightDependencies);

            foreach (var jobItem in _jobItems)
                jobItem.Initialize(questDependencies);
            
            foreach (var dialogueObject in _dialogueObjects)
                dialogueObject.Initialize(dialogueDependencies);

            _weeds = new()
            {
                new EyeOpeningWeed(),
                new DashWeed(),
                new DoubleJump()
            };
            
            foreach(var weed in _weeds)
                weed.Initialize(weedDependencies);

            for (int i = 0; i < _weeds.Count; i++)
                _weedInventorySlots[i].Initialize(_weeds[i]);
            
            _weedInventorySlotSelector.Initialize(_weedInventorySlots);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _weeds[0].ApplyEffect();
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _weeds[1].ApplyEffect();
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
                _weeds[2].ApplyEffect();
        }
    }
}