using UnityEngine;

namespace _Project.Scripts.Weed
{
    [CreateAssetMenu(fileName = "NewWeedScriptableObject", menuName = "ScriptableObjects/Weed")]
    public class WeedScriptableObject : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}