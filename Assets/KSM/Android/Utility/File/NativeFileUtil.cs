namespace KSM.Android.Utility
{
    using KSM.Android.Content;
    using UnityEngine;

    public static class NativeFileUtil
    {
        private static AndroidJavaClass _fileProviderClass = null;
        public static AndroidJavaClass fileProviderClass
        {
            get
            {
                if(_fileProviderClass == null)
                {
                    _fileProviderClass = new AndroidJavaClass("androidx.core.content.FileProvider");
                    if(_fileProviderClass == null)
                    {
                        Debug.LogError("Cannot find package:androidx.core.content.FileProvider\nPlease check the gradle dependency.");
                    }
                }
                return _fileProviderClass;
            }
        }

        public static void ViewFile(string fileName)
        {
#if UNITY_ANDROID
            if (Application.isEditor) return;

            //FileType fileType = GetFileType(fileName);

            //if(fileType == FileType.notSupported)
            //{
            //    Debug.LogError($"{fileName} is not supported file type!");
            //    return;
            //}

            //string intentType = "application/" + fileType.ToString();

            AndroidJavaObject currentActivity = NativeUnityUtil.currentActivity;

            NativeIntent intent = new NativeIntent("ACTION_VIEW");

            AndroidJavaObject uriObject = GetUri(currentActivity, currentActivity.Call<string>("getPackageName") + ".fileprovider", fileName);
            
            //NativeIntentUtil.SetDataAndType(intentObject, uriObject, intentType);
            intent.SetData(uriObject);
            intent.SetFlags("FLAG_ACTIVITY_CLEAR_TOP", "FLAG_GRANT_READ_URI_PERMISSION", "FLAG_ACTIVITY_NO_HISTORY");
            intent.CreateChooser("View File");
#else
            Debug.LogError("ViewFile is not supported in this platform.");
#endif
        }

        public static void SendFile(string fileName)
        {
#if UNITY_ANDROID
            if (Application.isEditor) return;

            string intentType = NativeIntent.GetMIMEType(fileName);

            AndroidJavaObject currentActivity = NativeUnityUtil.currentActivity;

            NativeIntent intent = new NativeIntent("ACTION_SEND");

            AndroidJavaObject uriObject = GetUri(currentActivity, currentActivity.Call<string>("getPackageName") + ".fileprovider", fileName);

            intent.PutExtraStream(uriObject);
            intent.SetType(intentType);
            intent.SetFlags("FLAG_ACTIVITY_CLEAR_TOP", "FLAG_GRANT_READ_URI_PERMISSION", "FLAG_ACTIVITY_NO_HISTORY");
            intent.CreateChooser("Send File");
#else
            Debug.LogError("ViewFile is not supported in this platform.");
#endif
        }
        public static AndroidJavaObject GetUri(AndroidJavaObject context, string authority, string fileName)
        {
            AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", fileName);

            object[] providerParams = new object[3];
            providerParams[0] = context;
            providerParams[1] = authority;
            providerParams[2] = fileObject;

            return fileProviderClass.CallStatic<AndroidJavaObject>("getUriForFile", providerParams);
        }
    }
}