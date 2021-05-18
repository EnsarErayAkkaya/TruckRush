using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.UI
{
    public class EntranceUI : MonoBehaviour
    {
        public void OnPlayButtonClicked()
        {
            SceneManager.LoadScene(1);
        }
    }
}