using System;

namespace _Project.Scripts.Player
{
    public class Wallet
    {
        private int _money;
        
        public int Money
        {
            get => _money;
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException();

                _money = value;
                
                if (_money < 0)
                    throw new InvalidOperationException();
            }
        }
    }
}