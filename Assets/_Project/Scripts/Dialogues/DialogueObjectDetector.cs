using UnityEngine;

namespace _Project.Scripts.Dialogues
{
    public class DialogueObjectDetector : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _distance;

        private void Update()
        {
            RaycastHit hit;

            if (!Input.GetKeyDown(KeyCode.E))
                return;
            
            if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _distance, ~6)) 
                return;
            
            if (hit.transform.TryGetComponent(out DialogueObject dialogueObject))
                dialogueObject.StartDialogue();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(_camera.transform.position, _camera.transform.forward * _distance);
        }
    }
}