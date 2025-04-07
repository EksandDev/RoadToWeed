using System;
using _Project.Scripts.Dialogues;
using UnityEngine;

namespace _Project.Scripts.Initialization
{
    public class TestBootstrap : MonoBehaviour
    {
        [SerializeField] private DialogueUI _dialogueUI;
        [SerializeField] private DialogueObject[] _dialogueObjects;
        
        private void Start()
        {
            _dialogueUI.Initialize(new());

            foreach (var dialogueObject in _dialogueObjects)
                dialogueObject.Initialize(_dialogueUI);
        }
    }
}