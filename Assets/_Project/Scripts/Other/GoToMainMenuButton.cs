using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Fight
{
    public class GoToMainMenuButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene(0);
        }
    }
}