using TMPro;
using UnityEngine;

namespace _Project.Scripts.Shop
{
    public class ShopButton : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _purchaseSound;
        [SerializeField] private AudioClip _clickSound;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;

        private Shop _shop;
        private Weed.Weed _item;

        #region Validate
        private void OnValidate()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }
        #endregion

        public void Initialize(Shop shop, Weed.Weed item)
        {
            _shop = shop;
            _item = item;
            _nameText.text = _item.Data.Name;
            _descriptionText.text = _item.Data.Description;
        }

        public void OnClick()
        {
            _audioSource.PlayOneShot(_shop.TryBuy(_item) ? _purchaseSound : _clickSound);
        }
    }
}