using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KSM.Android.Utility
{
    public static class NativeTTSUtil
    {
        private static AndroidJavaObject _ttsObject = null;
        public static AndroidJavaObject ttsObject
        {
            get
            {
                if(_ttsObject == null)
                {
                    _ttsObject = new AndroidJavaObject("android.speech.tts.TextToSpeech", NativeUnityUtil.currentActivity, new TTSOnInitListener());
                }

                return _ttsObject;
            }
        }

        public class TTSOnInitListener : AndroidJavaProxy
        {
            public TTSOnInitListener() : base("android.speech.tts.TextToSpeech$OnInitListener") { }

            public void onInit(int status)
            {
                if (status != TTSStatus.ERROR)
                {
                    AndroidJavaClass localeClass = new AndroidJavaClass("java.util.Locale");

                    ttsObject.Call<int>("setLanguage", localeClass.CallStatic<AndroidJavaObject>("getDefault"));
                }
            }
        }

        public static int Speak(string text, bool discardCurrentQueue = false)
        {
            if (discardCurrentQueue)
            {
                return ttsObject.Call<int>("speak", text, QueueMode.QUEUE_FLUSH, null);
            }
            else
            {
                return ttsObject.Call<int>("speak", text, QueueMode.QUEUE_ADD, null); 
            }
        }

        public static int Stop() => ttsObject.Call<int>("stop");

        /// <summary>
        /// Play Silence with duration
        /// </summary>
        /// <param name="durationInMs">unit: ms</param>
        /// <param name="discardCurrentQueue"></param>
        /// <returns></returns>
        public static int PlaySilence(long durationInMs, bool discardCurrentQueue)
        {
            if (discardCurrentQueue)
            {
                return ttsObject.Call<int>("playSilence", durationInMs, QueueMode.QUEUE_FLUSH, null);
            }
            else
            {
                return ttsObject.Call<int>("playSilence", durationInMs, QueueMode.QUEUE_ADD, null);
            }
        }

        /// <summary>
        /// Set Speech Rate
        /// </summary>
        /// <param name="speechRate">
        /// 1.0 is the normal speech rate,
        /// lower values slow down the speech 0.5 is half the normal speech rate,
        /// greater values accelerate it 2.0 is twice the normal speech rate.
        /// </param>
        /// <returns></returns>
        public static int SetSpeechRate(float speechRate)
        {
            return ttsObject.Call<int>("setSpeechRate", speechRate);
        }

        /// <summary>
        /// Set the speech pitch for the TTS engine.
        /// This has no effect on any pre-recorded speech.
        /// </summary>
        /// <param name="pitch">
        /// 1.0 is the normal pitch,
        /// lower values lower the tone of the synthesized voice,
        /// greater values increase it.
        /// </param>
        /// <returns></returns>
        public static int SetPitch(float pitch)
        {
            return ttsObject.Call<int>("setPitch", pitch);
        }

        public static bool IsSpeaking => ttsObject.Call<bool>("isSpeaking");
    }
}