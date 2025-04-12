using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Messages;
using _Project.Scripts.Player;
using _Project.Scripts.Weed;

namespace _Project.Scripts.Shop
{
    public class Shop
    {
        public event Action<Dictionary<string, int>> ItemPurchased;
        
        private NotificationSender _notificationSender;
        private WeedInventorySlotSelector _selector;
        private Wallet _wallet;
        private Dictionary<string, int> _purchasedWeeds = new();
        
        public Shop(NotificationSender notificationSender, WeedInventorySlotSelector selector, Wallet wallet)
        {
            _notificationSender = notificationSender;
            _selector = selector;
            _wallet = wallet;

            foreach (var slot in _selector.Slots)
                _purchasedWeeds.Add(slot.Weed.Data.Name, 0);
        }

        public bool TryBuy(Weed.Weed item)
        {
            foreach (var slot in _selector.Slots)
            {
                var itemInSlot = slot.Weed;

                if (item.Data.Name != itemInSlot.Data.Name)
                    continue;

                if (item.Data.Price > _wallet.Money)
                {
                    _notificationSender.Send("Недостаточно средств для покупки");
                    return false;
                }
                
                if (itemInSlot.Count + _purchasedWeeds[itemInSlot.Data.Name] >= itemInSlot.MaxCount)
                {
                    _notificationSender.Send($"{item.Data.Name} у тебя в достатке");
                    return false;
                }

                _purchasedWeeds[itemInSlot.Data.Name]++;
                _wallet.Money -= item.Data.Price;
                ItemPurchased?.Invoke(_purchasedWeeds);
                _notificationSender.Send($"Куплен {item.Data.Name}, в схроне: {_purchasedWeeds[itemInSlot.Data.Name]}");
                return true;
            }

            throw new InvalidOperationException();
        }

        public void ClearPurchasedWeeds()
        {
            _purchasedWeeds = new(); 
            
            foreach (var slot in _selector.Slots)
                _purchasedWeeds.Add(slot.Weed.Data.Name, 0);
        }
    }
}