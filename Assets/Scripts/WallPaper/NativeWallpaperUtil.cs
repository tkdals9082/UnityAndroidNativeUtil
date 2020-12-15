using UnityEngine;

namespace KSM.Android.Utility
{
    public static class NativeWallpaperUtil
    {
        public static void SetWallPaper(string absoluteFilePath)
        {
            AndroidJavaObject wallPaperManager = (new AndroidJavaClass("android.app.WallpaperManager")).CallStatic<AndroidJavaObject>("getInstance", NativeUnityUtil.currentActivity);

            AndroidJavaObject url = new AndroidJavaObject("java.net.URL", absoluteFilePath);

            AndroidJavaObject inputStream = url.Call<AndroidJavaObject>("openStream");

            wallPaperManager.Call("setStream", inputStream);
        }
    }
}