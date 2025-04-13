using System;
using _Project.Scripts.Quests;
using UnityEngine;

namespace _Project.Scripts.Dialogues
{
    public abstract class DialogueObject : MonoBehaviour
    {
        public abstract void Initialize(DialogueDependencies dialogueDependencies);
        public abstract void StartDialogue();
    }
}