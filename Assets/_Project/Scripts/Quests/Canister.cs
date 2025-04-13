using System;
using UnityEngine;

namespace _Project.Scripts.Quests
{
    public class Canister : MonoBehaviour
    {
        public event Action PickedUp;

        public void PickUp()
        {
            PickedUp?.Invoke();
            Destroy(gameObject);
        }
    }
}