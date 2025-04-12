using System;
using System.Collections.Generic;
using _Project.Scripts.Dialogues;
using _Project.Scripts.Fight;
using _Project.Scripts.Messages;
using _Project.Scripts.Other;
using _Project.Scripts.Player;
using _Project.Scripts.Quests;
using _Project.Scripts.Shop;
using _Project.Scripts.Weed;
using UnityEngine;

namespace _Project.Scripts.Initialization
{
    public class LevelEntryPoint : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerAttacker _playerAttacker;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private GameObject _hands;
            
        [Header("Dialogues")]
        [SerializeField] private DialogueUI _dialogueUI;
        [SerializeField] private DialogueWithoutAnswerScriptableObject _startDialogue;
        [SerializeField] private DialogueObject[] _dialogueObjects;

        [Header("Weed")] 
        [SerializeField] private int _amountOpenedWeed;
        [SerializeField] private int _weedAmountOnStart;
        [SerializeField] private WeedInventorySlotSelector _weedInventorySlotSelector;
        [SerializeField] private WeedInventorySlot[] _weedInventorySlots;
        [SerializeField] private WeedScriptableObject[] _weedData;
        [SerializeField] private GameObject[] _hiddenItems;

        [Header("Shop")] 
        [SerializeField] private ShopUI _shopUI;
        [SerializeField] private FindWeedObjectDetector _findWeedObjectDetector;
        [SerializeField] private ShopButton[] _shopButtons;

        [Header("Other")]
        [SerializeField] private QuestController _questController;
        [SerializeField] private Trailer _trailer;
        [SerializeField] private CoroutineStarter _coroutineStarter;
        [SerializeField] private NotificationSender _notificationSender;
        [SerializeField] private BlackScreen _blackScreen;
        [SerializeField] private JobItem[] _jobItems;

        private List<Weed.Weed> _weeds;
        
        private void Start()
        {
            LevelPlayerData levelPlayerData = new();
            _dialogueUI.Initialize(new(), _playerController, _playerAttacker, _shopUI, _hands);
            Wallet wallet = new();
            HiddenItemsController hiddenItemsController = new(_hiddenItems);
            DialogueDependencies dialogueDependencies = new(_dialogueUI, _notificationSender, levelPlayerData, wallet);
            QuestDependencies questDependencies = new(_notificationSender, levelPlayerData);
            FightDependencies fightDependencies = new(_notificationSender);
            WeedDependencies weedDependencies = new(_playerController, _playerAttacker, _coroutineStarter, _notificationSender,
                hiddenItemsController);
            _playerHealth.Initialize(fightDependencies, _hands, _blackScreen);

            foreach (var jobItem in _jobItems)
                jobItem.Initialize(questDependencies);
            
            foreach (var dialogueObject in _dialogueObjects)
                dialogueObject.Initialize(dialogueDependencies);

            _weeds = _amountOpenedWeed switch
            {
                1 => new() { new EyeOpeningWeed() },
                2 => new() { new EyeOpeningWeed(), new DashAndDoubleJumpWeed() },
                3 => new() { new EyeOpeningWeed(), new DashAndDoubleJumpWeed(), new FuryWeed() },
                _ => throw new InvalidOperationException()
            };

            for (int i = 0; i < _weedData.Length; i++)
                _weeds[i].Initialize(weedDependencies, _weedData[i]);

            for (int i = 0; i < _weeds.Count; i++)
                _weedInventorySlots[i].Initialize(_weeds[i]);
            
            _weedInventorySlotSelector.Initialize(_weedInventorySlots, _weedAmountOnStart);
            _shopUI.Initialize(wallet, _playerController, _playerAttacker, _weedInventorySlotSelector, _hands);
            Shop.Shop shop = new(_notificationSender, _weedInventorySlotSelector, wallet);

            for (var i = 0; i < _shopButtons.Length; i++)
            {
                var button = _shopButtons[i];
                button.Initialize(shop, _weeds[i]);
            }
            
            _findWeedObjectDetector.Initialize(_notificationSender, _weedInventorySlotSelector, shop);
            _questController.Initialize(_notificationSender, levelPlayerData);
            _trailer.Initialize(_notificationSender, levelPlayerData);
            _dialogueUI.ShowDialogue(_startDialogue.Text);
        }
    }
}