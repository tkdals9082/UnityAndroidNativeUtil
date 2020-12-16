namespace KSM.Android.Utility
{
    using UnityEngine;

    public class NativeUnityUtil
    {
        private static AndroidJavaClass _unityPlayer;

        public static AndroidJavaClass unityPlayer
        {
            get
            {
                if(_unityPlayer == null)
                {
                    _unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                }
                return _unityPlayer;
            }
        }

        public static AndroidJavaObject currentActivity => unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    }
}