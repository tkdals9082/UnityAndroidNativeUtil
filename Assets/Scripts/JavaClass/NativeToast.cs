using KSM.Android.Utility;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public static class NativeToast
{
    private static AndroidJavaClass _NativeToastClass = null;
    private static AndroidJavaClass NativeToastClass
    {
        get
        {
            if (_NativeToastClass == null)
            {
                _NativeToastClass = new AndroidJavaClass("android.widget.Toast");
            }
            return _NativeToastClass;
        }
    }

    public static void MakeText(string msg, int duration)
    {
        NativeUnityUtil.currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            NativeToastClass.CallStatic<AndroidJavaObject>("makeText", NativeUnityUtil.currentActivity, msg, duration).Call("show");
        }));
    }
}
