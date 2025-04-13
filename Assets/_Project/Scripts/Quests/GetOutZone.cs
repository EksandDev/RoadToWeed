using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Quests
{
    public class GetOutZone : MonoBehaviour
    {
        [SerializeField] private Transform _pointToTeleport;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out CharacterController characterController))
                return;

            characterController.enabled = false;
            characterController.transform.position = _pointToTeleport.position;
            characterController.enabled = true;
        }
    }
}