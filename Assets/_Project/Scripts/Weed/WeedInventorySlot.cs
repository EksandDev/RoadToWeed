using UnityEngine;

namespace _Project.Scripts.Weed
{
    public class WeedInventorySlot : MonoBehaviour
    {
        private Vector3 _originalScale;
        
        public Weed Weed { get; private set; }

        public void Initialize(Weed weed)
        {
            _originalScale = transform.localScale;
            Weed = weed;
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
    }
}