using UnityEngine;

namespace _Project.Scripts.Dialogues
{
    public abstract class DialogueObject : MonoBehaviour
    {
        [SerializeField] private Transform _pointToRotatePlayer;

        public abstract void Initialize(DialogueUI dialogueUI);
        public abstract void StartDialogue();
    }
}