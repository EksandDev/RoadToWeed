using System.Collections;
using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextDecryptEffect : MonoBehaviour
    {
        [Header("Decrypt Settings")]
        [SerializeField] private float _symbolChangeSpeed = 0.05f;
        [SerializeField] private float _finalTextDelay = 0.1f;
        [SerializeField] private string _finalText = "Hello, World!";

        private TextMeshProUGUI _text;
        private Coroutine _decryptCoroutine;
        private Color _originalColor;
        private bool _isDecrypting;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _originalColor = _text.color;
            _text.text = "";
        }

        private void Start()
        {
            StartDecryption();
        }

        private void StartDecryption()
        {
            if (_isDecrypting) 
                return;
        
            _isDecrypting = true;
            _text.text = "";
        
            if (_decryptCoroutine != null)
                StopCoroutine(_decryptCoroutine);
        
            _decryptCoroutine = StartCoroutine(DecryptTextRoutine());
        }

        private IEnumerator DecryptTextRoutine()
        {
            char[] _finalChars = _finalText.ToCharArray();
            char[] _currentChars = new char[_finalChars.Length];
        
            for (int i = 0; i < _currentChars.Length; i++)
                _currentChars[i] = GetRandomChar();

            var noiseCoroutine = StartCoroutine(GenerateNoiseRoutine(_currentChars, _finalChars));

            for (int i = 0; i < _finalChars.Length; i++)
            {
                yield return new WaitForSeconds(_finalTextDelay);
                _currentChars[i] = _finalChars[i];
                _text.text = new string(_currentChars);
            }

            StopCoroutine(noiseCoroutine);
            _text.color = _originalColor;
            _isDecrypting = false;
        
            StartDecryption();
        }

        private IEnumerator GenerateNoiseRoutine(char[] currentChars, char[] finalChars)
        {
            while (_isDecrypting)
            {
                for (int i = 0; i < currentChars.Length; i++)
                {
                    if (currentChars[i] != finalChars[i])
                    {
                        currentChars[i] = GetRandomChar();
                    }
                }
                _text.text = new string(currentChars);
                yield return new WaitForSeconds(_symbolChangeSpeed);
            }
        }

        private char GetRandomChar()
        {
            int randomType = Random.Range(0, 3);
            return randomType switch
            {
                0 => (char)Random.Range('A', 'Z' + 1),
                1 => (char)Random.Range('a', 'z' + 1),
                2 => (char)Random.Range('0', '9' + 1),
                _ => (char)Random.Range(33, 127)
            };
        }
    }
}
