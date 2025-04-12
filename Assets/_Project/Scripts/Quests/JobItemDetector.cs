using UnityEngine;

namespace _Project.Scripts.Quests
{
    public class JobItemDetector : MonoBehaviour
    {
        [SerializeField] private GameObject _jobItemDetectorUI;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _distance;

        private RaycastHit _hit;
        
        private void Update()
        {
            if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, _distance, ~6))
            {
                if (_jobItemDetectorUI.activeInHierarchy)
                    _jobItemDetectorUI.SetActive(false);
                    
                return;
            } 

            if (!_hit.transform.TryGetComponent(out JobItem jobItem)) 
                return;
            
            if (!_jobItemDetectorUI.activeInHierarchy)
                _jobItemDetectorUI.SetActive(true);

            if (!Input.GetKeyDown(KeyCode.E)) 
                return;
            
            jobItem.Interact();
        }
    }
}