using _Project.Scripts.Player;

namespace _Project.Scripts.Weed
{
    public class DashAndDoubleJumpWeed : Weed
    {
        private PlayerController _playerController;
        
        public override void Initialize(WeedDependencies weedDependencies, WeedScriptableObject data)
        {
            ActionTime = 20;
            _playerController = weedDependencies.PlayerController;
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
                _playerController.CanDoubleJump = true;
                _playerController.CanDash = true;
                CoroutineStarter.StartCoroutine(TimerCoroutine());
                NotificationSender.Send("Рывок и двойной прыжок доступны");
                return;
            }
            
            NotificationSender.Send("Эффект ещё не прошёл");
        }

        protected override void RemoveEffect()
        {
            IsReadyToApplyEffect = true;
            _playerController.CanDoubleJump = false;
            _playerController.CanDash = false;
            NotificationSender.Send("Рывок и двойной прыжок прошли");
        }
    }
}