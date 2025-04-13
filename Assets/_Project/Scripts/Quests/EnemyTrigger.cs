using _Project.Scripts.Fight;
using _Project.Scripts.Fight.Enemies;
using UnityEngine;

namespace _Project.Scripts.Quests
{
    public class EnemyTrigger : MonoBehaviour
    {
        [SerializeField] private EnemyAttacker[] _enemies;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerHealth playerHealth))
                return;
            
            foreach (var enemy in _enemies)
                enemy.Enable(playerHealth.transform);
        }
    }
}