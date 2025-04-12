using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Fight
{
    public class RestartLevelButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}