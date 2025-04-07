using UnityEngine;

namespace _Project.Scripts.Dialogues
{
    [CreateAssetMenu(fileName = "NewDialogue", menuName = "ScriptableObjects/DialogueWithAnswer")]
    public class DialogueWithAnswerScriptableObject : ScriptableObject
    {
        [field: SerializeField, TextArea(3, 6)] public string[] Text { get; private set; }
        [field: SerializeField] public string[] PlayerAnswers { get; private set; }
        [field: SerializeField, TextArea(3, 6)] public string[] FinalText { get; private set; }
    }
}