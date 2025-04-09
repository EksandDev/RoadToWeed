using _Project.Scripts.Player;
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
        
        public void Initialize(Wallet wallet, PlayerController playerController)
        {
            _wallet = wallet;
            _playerController = playerController;
            _moneyText.text = wallet.Money + "$";
            _wallet.MoneyChanged += i => _moneyText.text = i + "$";
        }
        
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.R))
                return;

            if (_panel.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.Locked;
                _panel.SetActive(false);
                _playerController.IsEnabled = true;
                return;
            }
            
            Cursor.lockState = CursorLockMode.None;
            _panel.SetActive(true);
            _playerController.IsEnabled = false;
        }
        
        private void OnDisable()
        {
            _wallet.MoneyChanged -= i => _moneyText.text = i.ToString();
        }
    }
}