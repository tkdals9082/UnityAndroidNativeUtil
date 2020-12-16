using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KSM.Android.Utility.Example
{
    public class ExampleTTS : ExampleBase
    {
        [Header("InputField")]
        public InputField textIpf;
        public InputField durationIpf;
        public InputField speechRateIpf;
        public InputField pitchIpf;

        [Header("Button")]
        public Button speakButton;
        public Button stopButton;
        public Button silenceButton;
        public Button setRateButton;
        public Button setPitchButton;

        [Header("Toggle")]
        public Toggle discardToggle;
        public Toggle isSpeakingToggle;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            speakButton.onClick.AddListener(() => NativeTTSUtil.Speak(textIpf.text, discardToggle.isOn));
            stopButton.onClick.AddListener(() => NativeTTSUtil.Stop());
            silenceButton.onClick.AddListener(() => NativeTTSUtil.PlaySilence(long.Parse(durationIpf.text), discardToggle.isOn));
            setRateButton.onClick.AddListener(() => NativeTTSUtil.SetSpeechRate(float.Parse(speechRateIpf.text)));
            setPitchButton.onClick.AddListener(() => NativeTTSUtil.SetPitch(float.Parse(pitchIpf.text)));
        }
        
        private void Update()
        {
            isSpeakingToggle.isOn = NativeTTSUtil.IsSpeaking;
        }
    }
}