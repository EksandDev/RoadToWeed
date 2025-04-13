using _Project.Scripts.Messages;
using _Project.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace _Project.Scripts.Quests
{
    public class Trailer : MonoBehaviour
    {
        private NotificationSender _notificationSender;
        private LevelPlayerData _levelPlayerData;
        
        public void Initialize(NotificationSender notificationSender, LevelPlayerData levelPlayerData)
        {
            _notificationSender = notificationSender;
            _levelPlayerData = levelPlayerData;
        }
        
        public void Interact()
        {
            if (!_levelPlayerData.ReadyToLeave)
            {
                _notificationSender.Send("Я здесь ещё не закончил");
                return;
            }
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}