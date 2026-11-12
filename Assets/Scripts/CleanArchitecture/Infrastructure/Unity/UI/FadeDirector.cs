using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ShoulderDelivery.Infrastructure
{
    public class FadeDirector : MonoBehaviour
    {
        [SerializeField] PlayableDirector _director;
        [SerializeField] TimelineAsset _fadeInAsset;
        [SerializeField] TimelineAsset _fadeOutAsset;

        public async UniTask FadeIn()
        {
            if (_director == null || _fadeInAsset == null) return;
            _director.Play(_fadeInAsset);
            await UniTask.WaitUntil(() => _director.time >= _director.duration);
        }

        public async UniTask FadeOut()
        {
            if (_director == null || _fadeOutAsset == null) return;
            _director.Play(_fadeOutAsset);
            await UniTask.WaitUntil(() => _director.time >= _director.duration);
        }
    }
}
