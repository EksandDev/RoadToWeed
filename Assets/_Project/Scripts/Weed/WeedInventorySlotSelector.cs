using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Weed
{
    public class WeedInventorySlotSelector : MonoBehaviour
    {
        private WeedInventorySlot[] _slots;

        public WeedInventorySlot[] Slots => _slots;

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

        private void SelectSlot(int index)
        {
            if (!_slots[index].IsReadyToSelect || _slots[index].Weed.Count <= 0)
            {
                _slots[index].Weed.ApplyEffect();
                return;
            }

            _slots[index].IsReadyToSelect = false;
            _slots[index].Select();
            _slots[index].Weed.ApplyEffect();
            StartCoroutine(DeselectCoroutine(index, _slots[index].Weed.ActionTime));
        }

        private IEnumerator DeselectCoroutine(int index, float actionTime)
        {
            yield return new WaitForSeconds(actionTime);
            _slots[index].IsReadyToSelect = true;
            _slots[index].Deselect();
        }
    }
}