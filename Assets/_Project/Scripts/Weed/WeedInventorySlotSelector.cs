using UnityEngine;

namespace _Project.Scripts.Weed
{
    public class WeedInventorySlotSelector : MonoBehaviour
    {
        private WeedInventorySlot[] _slots;
        private WeedInventorySlot _selectedSlot;

        public void Initialize(WeedInventorySlot[] slots)
        {
            _slots = slots;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectSlot(0);
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectSlot(1);
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SelectSlot(2);
        }

        public void DeselectSelectedSlot()
        {
            _selectedSlot.Deselect();
            _selectedSlot = null;
        }

        private void SelectSlot(int index)
        {
            if (_selectedSlot)
                DeselectSelectedSlot();

            _selectedSlot = _slots[index];
            _slots[index].Select();
        }
    }
}