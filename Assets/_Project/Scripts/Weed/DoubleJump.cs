using _Project.Scripts.Player;

namespace _Project.Scripts.Weed
{
    public class DoubleJump : Weed
    {
        private PlayerController _playerController;
        
        public override void Initialize(WeedDependencies weedDependencies)
        {
            ActionTime = 20;
            _playerController = weedDependencies.PlayerController;
            CoroutineStarter = weedDependencies.CoroutineStarter;
            NotificationSender = weedDependencies.NotificationSender;
        }

        public override void ApplyEffect()
        {
            if (IsReadyToApplyEffect)
            {
                IsReadyToApplyEffect = false;
                _playerController.CanDoubleJump = true;
                CoroutineStarter.StartCoroutine(TimerCoroutine());
                NotificationSender.Send("Доступен двойной прыжок");
                return;
            }
            
            NotificationSender.Send("Эффект ещё не прошёл");
        }

        protected override void RemoveEffect()
        {
            IsReadyToApplyEffect = true;
            _playerController.CanDoubleJump = false;
            NotificationSender.Send("Двойной прыжок прошёл");
        }
    }
}