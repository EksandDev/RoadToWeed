using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Weed
{
    public class WeedInventorySlot : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private Image _icon;
        
        private Vector3 _originalScale;
        private GameObject _parent;
        
        public Weed Weed { get; private set; }
        public bool IsReadyToSelect { get; set; } = true;

        public void Initialize(Weed weed)
        {
            _originalScale = transform.localScale;
            Weed = weed;
            _icon.sprite = Weed.Data.Icon;
            _parent = _countText.transform.parent.gameObject;
            Weed.CountChanged += OnWeedCountChanged;
            OnWeedCountChanged(Weed.Count);
        }

        public void Select()
        {
            var scale = transform.localScale;
            transform.localScale = new(scale.x + 0.1f, scale.y + 0.1f);
        }

        public void Deselect()
        {
            var scale = transform.localScale;
            transform.localScale = _originalScale;
        }

        private void OnWeedCountChanged(int count)
        {
            _countText.text = count.ToString();
            
            if (Weed.Count <= 0)
                _parent.SetActive(false);
            
            else if (!_parent.activeInHierarchy)
                _parent.SetActive(true);
        }
    }
}