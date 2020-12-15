using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace KSM.Android.Utility.Example
{
    class ExampleWallpaper : ExampleBase
    {
        public Button wallpaperButton1;
        public Button wallpaperButton2;
        public Button wallpaperButton3;

        protected override void Start()
        {
            base.Start();

            wallpaperButton1.onClick.AddListener(() => SetWallpaper("Wallpaper1.png"));
            wallpaperButton2.onClick.AddListener(() => SetWallpaper("Wallpaper2.png"));
            wallpaperButton3.onClick.AddListener(() => SetWallpaper("Wallpaper3.png"));
        }

        private void SetWallpaper(string fileName)
        {
            NativeWallpaperUtil.SetWallPaper(Path.Combine(Application.streamingAssetsPath, fileName));
            NativeToast.MakeText($"Change Wallpaper to {fileName}!", 700);
        }
    }
}