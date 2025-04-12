namespace _Project.Scripts.Weed
{
    public class EyeOpeningWeed : Weed
    {
        private HiddenItemsController _hiddenItemsController;
        
        public override void Initialize(WeedDependencies weedDependencies, WeedScriptableObject data)
        {
            ActionTime = 10;
            CoroutineStarter = weedDependencies.CoroutineStarter;
            NotificationSender = weedDependencies.NotificationSender;
            _hiddenItemsController = weedDependencies.HiddenItemsController;
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
                _hiddenItemsController.SetActive(true);
                CoroutineStarter.StartCoroutine(TimerCoroutine());
                NotificationSender.Send("Скрытое стало явным");
                return;
            }
            
            NotificationSender.Send("Эффект ещё не прошёл");
        }

        protected override void RemoveEffect()
        {
            IsReadyToApplyEffect = true;
            _hiddenItemsController.SetActive(false);
            NotificationSender.Send("Занавес вновь опустился");
        }
    }
}