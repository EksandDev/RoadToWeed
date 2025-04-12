using UnityEngine;

namespace _Project.Scripts.Quests
{
    public class TrailerDetector : MonoBehaviour
    {
        [SerializeField] private GameObject _trailerDetectorUI;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _distance;
        
        private RaycastHit _hit;
        
        private void Update()
        {
            if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, _distance, ~6))
            {
                if (_trailerDetectorUI.activeInHierarchy)
                    _trailerDetectorUI.SetActive(false);
                    
                return;
            } 

            if (!_hit.transform.TryGetComponent(out Trailer trailer)) 
                return;
            
            if (!_trailerDetectorUI.activeInHierarchy)
                _trailerDetectorUI.SetActive(true);

            if (!Input.GetKeyDown(KeyCode.E)) 
                return;
            
            trailer.Interact();
        }
    }
}