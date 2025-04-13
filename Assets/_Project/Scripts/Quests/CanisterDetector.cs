using UnityEngine;

namespace _Project.Scripts.Quests
{
    public class CanisterDetector : MonoBehaviour
    {
        [SerializeField] private GameObject _canisterDetectorUI;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _distance;
        
        private RaycastHit _hit;
        
        private void Update()
        {
            if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, _distance, ~6))
            {
                if (_canisterDetectorUI.activeInHierarchy)
                    _canisterDetectorUI.SetActive(false);
                    
                return;
            } 

            if (!_hit.transform.TryGetComponent(out Canister canister)) 
                return;
            
            if (!_canisterDetectorUI.activeInHierarchy)
                _canisterDetectorUI.SetActive(true);

            if (!Input.GetKeyDown(KeyCode.E)) 
                return;
            
            canister.PickUp();
        }
    }
}