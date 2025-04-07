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
        [SerializeField] private AnswerPanel _answerPanelPrefab;
        [SerializeField] private TMP_Text _dialogueText;
        [SerializeField] private PlayerController _playerController;

        private TextTyping _textTyping;
        private List<AnswerPanel> _spawnedAnswerPanels;
        private Coroutine _typingCoroutine;
        private bool _dialogueIsActive;

        public void Initialize(TextTyping textTyping)
        {
            _textTyping = textTyping;
        }

        public void ShowDialogue(string[] dialogueTexts)
        {
            if (_dialogueIsActive)
                return;
            
            _playerController.IsEnabled = false;
            _dialogueIsActive = true;
            _dialoguePopup.SetActive(true);
            StartCoroutine(TypeDialogue(dialogueTexts));
        }

        public void HideDialogue()
        {
            _playerController.IsEnabled = true;
            _dialogueIsActive = false;
            _dialogueText.text = "";
            _dialoguePopup.SetActive(false);
        }

        public void ShowAnswers(string[] answers)
        {
            if (answers.Length > 2)
                throw new InvalidOperationException();
            
            _answerPopup.SetActive(true);
            _spawnedAnswerPanels = new();

            for (int i = 0; i <= answers.Length; i++)
            {
                var spawnedAnswerPanel = Instantiate(_answerPanelPrefab, _answerPopup.transform);
                spawnedAnswerPanel.Text.text = answers[i];
                _spawnedAnswerPanels.Add(spawnedAnswerPanel); 
            }
        }

        public void HideAnswers()
        {
            foreach (var answerPanel in _spawnedAnswerPanels)
                Destroy(answerPanel);
            
            _answerPopup.SetActive(false);
            _spawnedAnswerPanels.Clear();
        }

        private IEnumerator TypeDialogue(string[] dialogueTexts)
        {
            int index = 1;
            _typingCoroutine = StartCoroutine(_textTyping.TypeText(_dialogueText, dialogueTexts[0]));
            
            while (true)
            {
                yield return null;
                
                if (!Input.GetKeyDown(KeyCode.Space)) 
                    continue;
                
                if (_typingCoroutine != null)
                    StopCoroutine(_typingCoroutine);
                
                if (index >= dialogueTexts.Length)
                {
                    HideDialogue();
                    yield break;
                }
                
                StartCoroutine(_textTyping.TypeText(_dialogueText, dialogueTexts[index]));
                index++;
            }
        }
    }
}