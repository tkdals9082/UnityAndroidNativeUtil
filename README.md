# UnityAndroidNativeUtil

Android features implemented by C#, Unity

I Create this library to stop switching IDEs (e.g Unity and Android Studio) when I develop an android feature

## Excel

Create Excel in android with C#
- Use [apache poi library](https://poi.apache.org/) to create excel file
- Implement functions only what I need. If you need another function, implement your own at [NativeExcelUtil.cs](https://github.com/tkdals9082/UnityAndroidNativeUtil/blob/main/Assets/Scripts/Excel/NativeExcelUtil.cs)

<br/>

## Text To Speech

Convert given text to speech in android
- Use [android.speech.tts.TextToSpeech](https://developer.android.com/reference/android/speech/tts/TextToSpeech) library
- Implement almost functions

<br/>

## Set Wallpaper

Set wallpaper with given image file
- Use [android.app.WallpaperManager](https://developer.android.com/reference/android/app/WallpaperManager) library
- Implement functions only what I need. If you need another function, implement your own at [NativeWallpaperUtil.cs](https://github.com/tkdals9082/UnityAndroidNativeUtil/blob/main/Assets/Scripts/WallPaper/NativeWallpaperUtil.cs)
- You should pass absolute path of the image. I didn't handle any exceptions :cold_sweat: 

## File

### Notice! To use this class, you should get the permission for external storage.

- View file with file path. You can choose file viewer.
- Send file with file path. You can choose file send application.

# Ongoing Implementation

- FileBrower
  - Send, View file in file browser

- Auto Speech Recognition
  - I already implement this one, but i forget where the base library came from :cold_sweat: 
  - I will upload this right after comfirm the license of base library.

- Serial Communication
  - Same as ASR... X(

# Known Bugs

# Contact

Email: tkdals9082@naver.com

[Join Slack](https://join.slack.com/t/w1608018452-fll264223/shared_invite/zt-k0mhaa4m-xFTosanIAiJdV2mS1~aKJA)