using _Project.Scripts.Dialogues;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Quests
{
    public abstract class JobItem : MonoBehaviour
    {
        protected LevelPlayerData LevelPlayerData { get; private set; }
        
        public void Initialize(LevelPlayerData levelPlayerData)
        {
            LevelPlayerData = levelPlayerData;
        }
        
        public abstract bool CheckReadyToInteract();
        public abstract void Interact();
    }
}