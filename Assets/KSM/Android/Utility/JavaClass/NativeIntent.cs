namespace KSM.Android.Content
{
    using KSM.Android.Utility;
    using System.IO;
    using System.Linq;
    using UnityEngine;

    public class NativeIntent
    {
        private AndroidJavaObject _intent = null;
        
        /// <summary>
        /// Call this object when you need to call a function which is not implemented yet.
        /// <para/> Or, you can just implement the method in this class! :)
        /// </summary>
        public AndroidJavaObject intent
        {
            get
            {
                if(_intent == null)
                {
                    _intent = new AndroidJavaObject("android.content.Intent");
                }
                return _intent;
            }
        }

        private AndroidJavaClass _intentClass = null;
        
        // caching intent class
        private AndroidJavaClass intentClass
        {
            get
            {
                if(_intentClass == null)
                {
                    _intentClass = new AndroidJavaClass("android.content.Intent");
                }
                return _intentClass;
            }
        }

        public NativeIntent(string actionType)
        {
            SetAction(actionType);
        }

        public NativeIntent SetAction(string actionType)
        {
            intent.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>(actionType));
            return this;
        }

        public NativeIntent SetData(AndroidJavaObject uri)
        {
            intent.Call<AndroidJavaObject>("setData", uri);
            return this;
        }

        public NativeIntent SetType(string type)
        {
            intent.Call<AndroidJavaObject>("setType", type);
            return this;
        }

        public NativeIntent SetDataAndType(AndroidJavaObject uri, string type)
        {
            intent.Call<AndroidJavaObject>("setDataAndType", uri, type);
            return this;
        }

        public NativeIntent SetFlags(params string[] flags)
        {
            if (flags.Length == 0) return this;

            intent.Call<AndroidJavaObject>("setFlags", flags.Select(flag => intentClass.GetStatic<int>(flag)).Aggregate(0, (current, bt) => current | bt));

            return this;
        }

        public NativeIntent AddFlags(params string[] flags)
        {
            if (flags.Length == 0) return this;

            intent.Call<AndroidJavaObject>("addFlags", flags.Select(flag => intentClass.GetStatic<int>(flag)).Aggregate(0, (current, bt) => current | bt));

            return this;
        }

        public NativeIntent PutExtraStream(AndroidJavaObject uri)
        {
            intent.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uri);

            return this;
        }

        public void CreateChooser(string title)
        {
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intent, title);
            NativeUnityUtil.currentActivity.Call("startActivity", chooser);
        }

        /*
         * Add overloading of putExtra here. there are too many...
         * 
         * 
         * 
         * */

        #region Utility
        public static string GetMIMEType(string fileName)
        {
            string extension = Path.GetExtension(fileName);

            string mimeType = "";

            switch (extension)
            {
                case ".txt":
                case ".html":
                case ".json":
                case ".xml":
                case ".cs":
                case ".py":
                case ".cpp":
                case ".h":
                case ".c":
                    // add whatever text type here
                    mimeType = "text/*";
                    break;
                case ".jpg":
                case ".png":
                case ".gif":
                case ".jpeg":
                case ".jpe":
                case ".jfif":
                    mimeType = "image/*";
                    break;
                case ".mp4":
                case ".3gp":
                case ".mkv":
                    mimeType = "video/*";
                    break;
                case ".pdf":
                    mimeType = "application/pdf";
                    break;
                case ".xlsx":
                case ".xls":
                case ".ods":
                case ".xltx":
                case ".xlsm":
                case ".xlsb":
                case ".csv":
                case ".prn":
                    mimeType = "application/excel";
                    break;
            }

            return mimeType;
        }
        #endregion
    }
}