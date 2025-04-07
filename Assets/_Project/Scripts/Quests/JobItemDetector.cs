using UnityEngine;

namespace _Project.Scripts.Quests
{
    public class JobItemDetector : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _distance;
        
        private void Update()
        {
            RaycastHit hit;

            if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _distance, ~6)) 
                return;

            if (!hit.transform.TryGetComponent(out JobItem jobItem)) 
                return;
            
            if (!jobItem.CheckReadyToInteract())
            {
                //вывести сообщение
                return;
            }

            if (jobItem.CheckReadyToInteract()) {}

            if (!Input.GetKeyDown(KeyCode.E)) 
                return;
            
            jobItem.Interact();
        }
    }
}