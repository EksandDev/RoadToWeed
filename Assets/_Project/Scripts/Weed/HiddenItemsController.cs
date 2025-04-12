using UnityEngine;

namespace _Project.Scripts.Weed
{
    public class HiddenItemsController
    {
        private GameObject[] _hiddenItems;
        
        public HiddenItemsController(GameObject[] hiddenItems)
        {
            _hiddenItems = hiddenItems;
            SetActive(false);
        }

        public void SetActive(bool value)
        {
            foreach (var item in _hiddenItems)
                item.SetActive(value);
        }
    }
}