using UnityEngine;

namespace _Project.Scripts.Other
{
    public class QuitButton : MonoBehaviour
    {
        public void OnClick()
        {
            Application.Quit();
        }
    }
}