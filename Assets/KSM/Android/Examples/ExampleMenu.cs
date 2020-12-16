namespace KSM.Android.Utility.Example
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class ExampleMenu : MonoBehaviour
    {
        public Button excelButton;
        public Button TTSButton;
        public Button wallpaperButton;
        // Start is called before the first frame update
        void Start()
        {
            excelButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("ExcelTest");
            });
            TTSButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("TTSTest");
            });
            wallpaperButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("WallpaperTest");
            });
        }
    }
}