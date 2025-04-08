using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Fight
{
    public abstract class Attacker : MonoBehaviour
    {
        [SerializeField] private float _damage;

        protected bool ReadyToAttack { get; set; } = true;

        protected abstract void Attack(IDamageable target);
        protected abstract IEnumerator TimerCoroutine();
        
        protected void DealDamage(IDamageable target)
        {
            target.TakeDamage(_damage);
        }
    }
}