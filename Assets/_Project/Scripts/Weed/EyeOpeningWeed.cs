namespace _Project.Scripts.Weed
{
    public class EyeOpeningWeed : Weed
    {
        private HiddenItemsController _hiddenItemsController;
        
        public override void Initialize(WeedDependencies weedDependencies)
        {
            ActionTime = 10;
            CoroutineStarter = weedDependencies.CoroutineStarter;
            NotificationSender = weedDependencies.NotificationSender;
            _hiddenItemsController = weedDependencies.HiddenItemsController;
        }

        public override void ApplyEffect()
        {
            if (IsReadyToApplyEffect)
            {
                IsReadyToApplyEffect = false;
                _hiddenItemsController.SetActive(true);
                CoroutineStarter.StartCoroutine(TimerCoroutine());
                NotificationSender.Send("Открытие глаз активировано");
                return;
            }
            
            NotificationSender.Send("Эффект ещё не прошёл");
        }

        protected override void RemoveEffect()
        {
            IsReadyToApplyEffect = true;
            _hiddenItemsController.SetActive(false);
            NotificationSender.Send("Открытие глаз прошло");
        }
    }
}