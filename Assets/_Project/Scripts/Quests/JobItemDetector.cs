using UnityEngine;

namespace _Project.Scripts.Quests
{
    public class JobItemDetector : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _distance;

        private RaycastHit _hit;
        
        private void Update()
        {
            if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, _distance, ~6)) 
                return;

            if (!_hit.transform.TryGetComponent(out JobItem jobItem)) 
                return;
            
            if (jobItem.CheckReadyToInteract())
            {
                //покрасить
            }

            if (!Input.GetKeyDown(KeyCode.E)) 
                return;
            
            jobItem.Interact();
        }
    }
}