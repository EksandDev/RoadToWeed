using _Project.Scripts.Dialogues;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Other
{
    public class End : MonoBehaviour
    {
        [SerializeField, TextArea(3, 6)] private string _fullText;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private AudioClip _dialogueSound;
        
        private TextTyping _textTyping = new();
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            StartCoroutine(_textTyping.TypeText(_text, _fullText, _audioSource, _dialogueSound));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Application.Quit();
        }
    }
}