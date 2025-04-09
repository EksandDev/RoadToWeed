using System;

namespace _Project.Scripts.Player
{
    public class Wallet
    {
        public event Action<int> MoneyChanged;
        
        private int _money = 100;
        
        public int Money
        {
            get => _money;
            set
            {
                if (value < 0)
                    throw new InvalidOperationException();

                _money = value;
                MoneyChanged?.Invoke(_money);
                
                if (_money < 0)
                    throw new InvalidOperationException();
            }
        }
    }
}