using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Dialogues
{
    public class TextTyping
    {
        private Coroutine _typingCoroutine;
        private const float _charsPerSecond = 10f;
        private const float _delayAfterComplete = 1f;

        public IEnumerator TypeText(TMP_Text text, string fullText, AudioSource audioSource, AudioClip sound)
        {
            text.text = "";
            const float delay = 1f / _charsPerSecond;

            for (int i = 0; i <= fullText.Length; i++)
            {
                text.text = fullText.Substring(0, i);
                audioSource.PlayOneShot(sound);
                
                if (i > 0 && i < fullText.Length && fullText[i] == ' ') 
                    continue;
                
                yield return new WaitForSeconds(delay);
            }
        }
    }
}