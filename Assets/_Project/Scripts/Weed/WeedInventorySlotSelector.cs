using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Weed
{
    public class WeedInventorySlotSelector : MonoBehaviour
    {
        private bool _isEnabled = true;
        
        public WeedInventorySlot[] Slots { get; private set; }
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                gameObject.SetActive(_isEnabled);
            }
        } 

        public void Initialize(WeedInventorySlot[] slots, int weedAmountOnStart)
        {
            Slots = slots;

            foreach (var slot in Slots)
                slot.Weed.Count = weedAmountOnStart;
        }
        
        private void Update()
        {
            if (!IsEnabled)
                return;
            
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
            if (!Slots[index].IsReadyToSelect || Slots[index].Weed.Count <= 0)
            {
                Slots[index].Weed.ApplyEffect();
                return;
            }

            Slots[index].IsReadyToSelect = false;
            Slots[index].Select();
            Slots[index].Weed.ApplyEffect();
            StartCoroutine(DeselectCoroutine(index, Slots[index].Weed.ActionTime));
        }

        private IEnumerator DeselectCoroutine(int index, float actionTime)
        {
            yield return new WaitForSeconds(actionTime);
            Slots[index].IsReadyToSelect = true;
            Slots[index].Deselect();
        }
    }
}