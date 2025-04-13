using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Other
{
    public class GoToGameButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene(1);
        }
    }
}