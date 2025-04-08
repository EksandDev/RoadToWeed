using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Messages
{
    public class Notification : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private Image _transformImage; 
        
        public string Message { get; set; }
        
        public void Initialize()
        {
            _text.text = Message;
            DOTween.Sequence().
                Append(transform.DOScale(1.2f, 0.5f)).
                Append(transform.DOScale(0.9f, 0.5f)).
                Append(transform.DOScale(1, 0.5f)).
                SetLink(gameObject);
            StartCoroutine(DisappearCoroutine());
        }

        private IEnumerator DisappearCoroutine()
        {
            yield return new WaitForSeconds(3);
            _text.DOFade(0.1f, 4).SetLink(gameObject);
        } 
    }
}