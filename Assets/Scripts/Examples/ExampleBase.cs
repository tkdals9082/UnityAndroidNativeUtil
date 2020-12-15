namespace KSM.Android.Utility.Example
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class ExampleBase : MonoBehaviour
    {
        public Button mainMenuButton;

        protected virtual void Start()
        {
            mainMenuButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Menu");
            });
        }
    }
}