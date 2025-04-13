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

        public void Enable(bool needAnimation = true)
        {
            if (needAnimation)
            {
                _blackScreenImage.DOFade(1, AnimationTime);
                return;
            }

            var newColor = _blackScreenImage.color;
            newColor.a = 1;
            _blackScreenImage.color = newColor;
        }

        public void Disable(bool needAnimation = true)
        {
            if (needAnimation)
            {
                _blackScreenImage.DOFade(0, AnimationTime);
                return;
            }
            
            var newColor = _blackScreenImage.color;
            newColor.a = 0;
            _blackScreenImage.color = newColor;
        }
    }
}