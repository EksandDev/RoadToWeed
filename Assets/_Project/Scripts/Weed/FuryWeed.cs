using _Project.Scripts.Fight;

namespace _Project.Scripts.Weed
{
    public class FuryWeed : Weed
    {
        private PlayerAttacker _playerAttacker;
        
        public override void Initialize(WeedDependencies weedDependencies, WeedScriptableObject data)
        {
            ActionTime = 20;
            _playerAttacker = weedDependencies.PlayerAttacker;
            CoroutineStarter = weedDependencies.CoroutineStarter;
            NotificationSender = weedDependencies.NotificationSender;
            Data = data;
        }

        public override void ApplyEffect()
        {
            if (IsReadyToApplyEffect)
            {
                if (Count <= 0)
                {
                    NotificationSender.Send($"{Data.Name} кончился");
                    return;
                }
                
                Count--;
                IsReadyToApplyEffect = false;
                _playerAttacker.CanStrongAttack = true;
                CoroutineStarter.StartCoroutine(TimerCoroutine());
                NotificationSender.Send("Сильный удар доступен");
                return;
            }
            
            NotificationSender.Send("Эффект ещё не прошёл");
        }

        protected override void RemoveEffect()
        {
            IsReadyToApplyEffect = true;
            _playerAttacker.CanStrongAttack = false;
            NotificationSender.Send("Сильный удар прошёл");
        }
    }
}