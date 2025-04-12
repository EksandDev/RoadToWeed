using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Quests
{
    public class GetOutZone : MonoBehaviour
    {
        [SerializeField] private Transform _pointToTeleport;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerController playerController))
                return;

            playerController.transform.position = _pointToTeleport.position;
        }
    }
}