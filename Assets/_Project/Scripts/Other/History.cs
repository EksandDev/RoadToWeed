using System;
using System.Collections;
using _Project.Scripts.Dialogues;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Other
{
    [RequireComponent(typeof(AudioSource))]
    public class History : MonoBehaviour
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}