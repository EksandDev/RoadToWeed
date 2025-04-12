using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Fight.Enemies
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private EnemyHealth _enemyHealth;

        private bool _isDead;
        
        private const string _isRunningAnimator = "IsRunning";
        
        public NavMeshAgent Agent { get; private set; }
        public Transform Target { get; set; }

        private void Start()
        {
            Agent = GetComponent<NavMeshAgent>();
            _enemyHealth.Died += OnDead;
        }

        private void Update()
        {
            if (_isDead)
                return;
            
            if (!Target && _animator.GetBool(_isRunningAnimator))
            {
                _animator.SetBool(_isRunningAnimator, false);
                return;
            }
            
            if (!_animator.GetBool(_isRunningAnimator))
                _animator.SetBool(_isRunningAnimator, true);
            
            Agent.SetDestination(Target.position);

            if (Vector3.Distance(Target.position, transform.position) <= Agent.stoppingDistance + 0.3f)
                _animator.SetBool(_isRunningAnimator, false);
        }

        private void OnDead()
        {
            _isDead = true;
            Agent.isStopped = true;
        }
    }
}