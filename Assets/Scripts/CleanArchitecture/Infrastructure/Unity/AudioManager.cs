using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] AudioData[] _clips;
        readonly Dictionary<string, AudioClip> _clipByName = new(StringComparer.Ordinal);
        readonly HashSet<AudioSource> _playingSources = new();
        AudioSource _bgmSource;

        internal static AudioManager Current { get; private set; }

        private void Awake()
        {
            if (_clips == null) return;

            foreach (var data in _clips)
            {
                if (data == null || string.IsNullOrWhiteSpace(data.ClipName) || data.Clip == null) continue;
                if (!_clipByName.TryAdd(data.ClipName, data.Clip))
                    Debug.LogWarning($"オーディオの名前が重複しています: {data.ClipName}", this);
            }
        }

        private void OnEnable()
        {
            if (Current != null && Current != this)
            {
                Debug.LogWarning("AudioManagerはシーンに1つ配置してください。", this);
                enabled = false;
                return;
            }
            Current = this;
        }

        private void OnDisable()
        {
            if (Current != this) return;

            foreach (var source in _playingSources)
                if (source != null) source.Stop();
            _playingSources.Clear();
            _bgmSource = null;
            Current = null;
        }

        internal void Play(AudioPublishData data)
        {
            var source = data.AudioSource;
            if (source == null || !source.isActiveAndEnabled) return;

            var clip = source.clip;
            if (!string.IsNullOrWhiteSpace(data.ClipName))
                _clipByName.TryGetValue(data.ClipName, out clip);
            if (clip == null)
            {
                Debug.LogWarning($"再生するAudioClipが見つかりません: {data.ClipName}", source);
                return;
            }

            var isBgm = data.AudioType == AudioType.OnShotBGM || data.AudioType == AudioType.LoopBGM;
            var loop = data.AudioType == AudioType.LoopBGM || data.AudioType == AudioType.LoopSE;

            if (isBgm && _bgmSource != null && _bgmSource != source)
                Stop(_bgmSource);

            _playingSources.Add(source);
            if (isBgm) _bgmSource = source;
            else if (_bgmSource == source) _bgmSource = null;

            // 単発SEは重ねて鳴らせる。ループの切り替えでは前の再生を止める
            if (data.AudioType == AudioType.OnShotSE)
            {
                if (source.loop) source.Stop();
                source.loop = false;
                source.PlayOneShot(clip);
                return;
            }

            if (loop && source.loop && source.isPlaying && source.clip == clip) return;

            source.Stop();
            source.clip = clip;
            source.loop = loop;
            source.Play();
        }

        internal void Stop(AudioSource source)
        {
            if (!_playingSources.Remove(source)) return;

            if (source != null) source.Stop();
            if (_bgmSource == source) _bgmSource = null;
        }

        [Serializable]
        class AudioData
        {
            [SerializeField] string _clipName;
            [SerializeField] AudioClip _clip;

            public string ClipName => _clipName;
            public AudioClip Clip => _clip;
        }
    }
}
