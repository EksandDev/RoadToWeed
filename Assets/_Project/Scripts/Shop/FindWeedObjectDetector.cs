using System.Collections.Generic;
using _Project.Scripts.Messages;
using _Project.Scripts.Weed;
using UnityEngine;

namespace _Project.Scripts.Shop
{
    public class FindWeedObjectDetector : MonoBehaviour
    {
        [SerializeField] private GameObject _findWeedObjectDetectorUI;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _distance;

        private NotificationSender _notificationSender;
        private WeedInventorySlotSelector _selector;
        private Shop _shop;
        private RaycastHit _hit;
        private Dictionary<string, int> _purchasedWeeds;

        public void Initialize(NotificationSender notificationSender, WeedInventorySlotSelector selector, Shop shop)
        {
            _notificationSender = notificationSender;
            _selector = selector;
            _shop = shop;
            _shop.ItemPurchased += ints => _purchasedWeeds = ints;
        }
        
        private void Update()
        {
            if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, _distance, ~6))
            {
                if (_findWeedObjectDetectorUI.activeInHierarchy)
                    _findWeedObjectDetectorUI.SetActive(false);
                
                return;
            } 

            if (!_hit.transform.TryGetComponent(out FindWeedObject findWeedObject))
                return;
            
            if (!_findWeedObjectDetectorUI.activeInHierarchy) 
                _findWeedObjectDetectorUI.SetActive(true);
            
            if (!Input.GetKeyDown(KeyCode.E))
                return;

            if (_purchasedWeeds == null)
            {
                _notificationSender.Send("Здесь ничего нет");
                return;
            }

            foreach (var purchasedWeed in _purchasedWeeds)
            {
                foreach (var slot in _selector.Slots)
                {
                    if (slot.Weed.Data.Name != purchasedWeed.Key || purchasedWeed.Value <= 0)
                        continue;
                    
                    slot.Weed.Count += purchasedWeed.Value;
                    _notificationSender.Send($"Подобран {purchasedWeed.Key} в количестве: {purchasedWeed.Value}");
                }
            }
            
            _shop.ClearPurchasedWeeds();
            _purchasedWeeds = null;
        }
    }
}