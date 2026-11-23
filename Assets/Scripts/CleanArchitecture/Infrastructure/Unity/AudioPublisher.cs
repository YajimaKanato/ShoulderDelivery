using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPublisher : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField, Tooltip("AudioManagerに登録したクリップ名。空欄ならAudioSourceのクリップを使用")]
        string _clipName;
        [SerializeField] AudioType _audioType;

        private void Awake()
        {
            if (_audioSource == null) _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        public void PlayAudio()
        {
            if (!isActiveAndEnabled) return;

            var manager = AudioManager.Current;
            if (manager == null)
            {
                Debug.LogWarning("AudioManagerがシーンにありません。", this);
                return;
            }
            manager.Play(new AudioPublishData(_audioSource, _clipName, _audioType));
        }

        public void StopAudio()
        {
            AudioManager.Current?.Stop(_audioSource);
        }

        private void OnDisable() => StopAudio();

        private void Reset()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }
    }
}
