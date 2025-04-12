namespace _Project.Scripts.Dialogues
{
    public abstract class DialogueAndAnswerObject : DialogueObject
    {
        public abstract void ReceivePlayerAnswer(int buttonIndex);
    }
}