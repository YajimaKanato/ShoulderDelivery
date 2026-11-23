using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public readonly struct AudioPublishData
    {
        public readonly AudioSource AudioSource;
        public readonly string ClipName;
        public readonly AudioType AudioType;

        public AudioPublishData(AudioSource audioSource = null
            , string clipName = null
            , AudioType audioType = AudioType.LoopBGM)
        {
            AudioSource = audioSource;
            ClipName = clipName;
            AudioType = audioType;
        }
    }

    public enum AudioType
    {
        OnShotBGM,
        LoopBGM,
        OnShotSE,
        LoopSE,
    }
}
