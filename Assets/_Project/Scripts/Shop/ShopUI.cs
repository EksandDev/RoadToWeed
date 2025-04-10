using _Project.Scripts.Fight;
using _Project.Scripts.Player;
using _Project.Scripts.Weed;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Shop
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _moneyText;

        private Wallet _wallet;
        private PlayerController _playerController;
        private PlayerAttacker _playerAttacker;
        private WeedInventorySlotSelector _weedInventorySlotSelector;
        private GameObject _hands;

        public bool IsEnabled { get; set; } = true;
        
        public void Initialize(Wallet wallet, PlayerController playerController, PlayerAttacker playerAttacker, 
            WeedInventorySlotSelector weedInventorySlotSelector, GameObject hands)
        {
            _wallet = wallet;
            _playerController = playerController;
            _playerAttacker = playerAttacker;
            _weedInventorySlotSelector = weedInventorySlotSelector;
            _hands = hands;
            _moneyText.text = wallet.Money + "$";
            _wallet.MoneyChanged += i => _moneyText.text = i + "$";
        }
        
        private void Update()
        {
            if (!IsEnabled)
                return;
            
            if (!Input.GetKeyDown(KeyCode.R))
                return;

            if (_panel.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.Locked;
                _panel.SetActive(false);
                _hands.SetActive(true);
                _playerController.IsEnabled = true;
                _playerAttacker.IsEnabled = true;
                _weedInventorySlotSelector.IsEnabled = true;
                return;
            }
            
            Cursor.lockState = CursorLockMode.None;
            _panel.SetActive(true);
            _hands.SetActive(false);
            _playerController.IsEnabled = false;
            _playerAttacker.IsEnabled = false;
            _weedInventorySlotSelector.IsEnabled = false;
        }
        
        private void OnDisable()
        {
            _wallet.MoneyChanged -= i => _moneyText.text = i.ToString();
        }
    }
}