using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Dialogues
{
    public class AnswerButton : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text Text { get; private set; }

        private Button _button;
        private DialogueAndAnswerObject _owner;
        private int _index;

        public void Initialize(int index, DialogueAndAnswerObject owner)
        {
            _index = index;
            _owner = owner;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _owner.ReceivePlayerAnswer(_index);
        }
    }
}