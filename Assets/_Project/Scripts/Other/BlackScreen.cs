using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Other
{
    public class BlackScreen : MonoBehaviour
    {
        [field: SerializeField] public float AnimationTime { get; private set; }

        private Image _blackScreenImage;
        
        private void Awake()
        {
            _blackScreenImage = GetComponent<Image>();
        }

        public void Enable()
        {
            _blackScreenImage.DOFade(1, AnimationTime);
        }

        public void Disable()
        {
            _blackScreenImage.DOFade(0, AnimationTime);
        }
    }
}