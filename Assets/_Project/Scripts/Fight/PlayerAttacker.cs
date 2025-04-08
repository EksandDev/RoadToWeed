using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Fight
{
    public class PlayerAttacker : Attacker
    {
        [SerializeField] private float _distance;
        [SerializeField] private float _cooldown;
        [SerializeField] private Camera _camera;

        private RaycastHit _hit;
        
        private void Update()
        {
            if (!ReadyToAttack || !Input.GetMouseButtonDown(0))
                return;
            
            if (!Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, _distance, ~6)) 
                return;
            
            if (_hit.transform.TryGetComponent(out IDamageable target))
                Attack(target);
        }

        protected override void Attack(IDamageable target)
        {
            ReadyToAttack = false;
            DealDamage(target);
            StartCoroutine(TimerCoroutine());
        }
        
        protected override IEnumerator TimerCoroutine()
        {
            yield return new WaitForSeconds(_cooldown);
            ReadyToAttack = true;
        }
    }
}