using UnityEngine;

public static class NativeExtensions
{
    private static AndroidJavaObject ClassForName(string className)
    {
        using (var clazz = new AndroidJavaClass("java.lang.Class"))
        {
            return clazz.CallStatic<AndroidJavaObject>("forName", className);
        }
    }

    public static AndroidJavaObject Cast(this AndroidJavaObject source, string destClass)
    {
        using (var destClassAJC = ClassForName(destClass))
        {
            return destClassAJC.Call<AndroidJavaObject>("cast", source);
        }
    }
}
