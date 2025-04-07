using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Player;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Dialogues
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private GameObject _dialoguePopup;
        [SerializeField] private GameObject _answerPopup;
        [SerializeField] private AnswerButton answerButtonPrefab;
        [SerializeField] private TMP_Text _dialogueText;
        [SerializeField] private PlayerController _playerController;

        private TextTyping _textTyping;
        private List<AnswerButton> _spawnedAnswerButtons;
        private Coroutine _typeTextCoroutine;
        private Coroutine _dialogueCoroutine;
        private bool _dialogueIsActive;

        public void Initialize(TextTyping textTyping)
        {
            _textTyping = textTyping;
        }

        public void ShowDialogue(string[] dialogueTexts, string[] answers = null, DialogueAndAnswerObject sender = null)
        {
            if (_dialogueIsActive)
                return;
            
            _playerController.IsEnabled = false;
            _dialogueIsActive = true;
            _dialoguePopup.SetActive(true);
            _dialogueCoroutine = StartCoroutine(TypeDialogue(dialogueTexts, answers, sender));
        }

        public void HideDialogue()
        {
            _playerController.IsEnabled = true;
            _dialogueIsActive = false;
            _dialogueText.text = "";
            _dialoguePopup.SetActive(false);
            
            if (_typeTextCoroutine != null)
                StopCoroutine(_typeTextCoroutine);
            
            if (_dialogueCoroutine != null)
                StopCoroutine(_dialogueCoroutine);
        }
        
        public void HideAnswers()
        {
            foreach (var answerButton in _spawnedAnswerButtons)
                Destroy(answerButton.gameObject);
            
            _answerPopup.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            _spawnedAnswerButtons.Clear();
        }

        private void ShowAnswers(string[] answers, DialogueAndAnswerObject sender)
        {
            if (answers.Length > 2)
                throw new InvalidOperationException();

            if (!sender)
                throw new NullReferenceException();
            
            _answerPopup.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            _spawnedAnswerButtons = new();

            for (int i = 0; i < answers.Length; i++)
            {
                var spawnedAnswerPanel = Instantiate(answerButtonPrefab, _answerPopup.transform);
                spawnedAnswerPanel.Initialize(i, sender);
                spawnedAnswerPanel.Text.text = answers[i];
                _spawnedAnswerButtons.Add(spawnedAnswerPanel); 
            }
        }

        private IEnumerator TypeDialogue(string[] dialogueTexts, string[] answers = null, DialogueAndAnswerObject sender = null)
        {
            int index = 1;
            _typeTextCoroutine = StartCoroutine(_textTyping.TypeText(_dialogueText, dialogueTexts[0]));
            
            while (true)
            {
                yield return null;
                
                if (!Input.GetKeyDown(KeyCode.Space)) 
                    continue;
                
                if (_typeTextCoroutine != null)
                    StopCoroutine(_typeTextCoroutine);

                if (index == dialogueTexts.Length - 1 && answers != null)
                    ShowAnswers(answers, sender);
                
                if (index >= dialogueTexts.Length)
                {
                    if (answers != null)
                        HideAnswers();
                    
                    HideDialogue();
                    yield break;
                }
                
                _typeTextCoroutine = StartCoroutine(_textTyping.TypeText(_dialogueText, dialogueTexts[index]));
                index++;
            }
        }
    }
}