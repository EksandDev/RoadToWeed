using _Project.Scripts.Messages;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Quests
{
    public class CanistersController : QuestController
    {
        [SerializeField] private Canister[] _canisters;

        private int _pickedUpCanisters;
        private NotificationSender _notificationSender;
        private LevelPlayerData _levelPlayerData;
        
        public override void Initialize(NotificationSender notificationSender, LevelPlayerData levelPlayerData)
        {
            _notificationSender = notificationSender;
            _levelPlayerData = levelPlayerData;
            
            foreach (var item in _canisters)
                item.PickedUp += OnCanisterPickedUp;
        }

        private void OnCanisterPickedUp()
        {
            if (_levelPlayerData.ReadyToLeave)
                return;
            
            _pickedUpCanisters++;

            if (_pickedUpCanisters == _canisters.Length)
            {
                _levelPlayerData.ReadyToLeave = true;
                _notificationSender.Send($"Все канистры собраны, можно уезжать");
                return;
            }
            
            _notificationSender.Send($"Канистра подобрана. Осталось ещё: {_canisters.Length - _pickedUpCanisters}");
        }
    }
}