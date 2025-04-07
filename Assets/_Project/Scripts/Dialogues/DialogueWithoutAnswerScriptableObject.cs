using UnityEngine;

namespace _Project.Scripts.Dialogues
{
    [CreateAssetMenu(fileName = "NewDialogue", menuName = "ScriptableObjects/DialogueWithoutAnswer")]
    public class DialogueWithoutAnswerScriptableObject : ScriptableObject
    {
        [field: SerializeField, TextArea(3, 6)] public string[] Text { get; private set; }
    }
}