using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class CardboardContextText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _text;
        [SerializeField] float _lifeTime = 1f;
        [SerializeField] float _duration = 0.3f;
        Tween _tween;

        bool IsAssigned => _text != null;

        private void Awake()
        {
            if (_text == null)
                UILogger.LogNotAssigned(_text);
        }

        public void SetColor(Color color)
        {
            if (!IsAssigned) return;

            _text.color = color;
        }

        public void EntryTween(float goal)
        {
            if (!IsAssigned) return;

            // 入りの演出
            _text.transform.DOMoveX(goal, _duration).SetRelative();
            // lifeTime終了時に元のX座標に戻る予約
            _tween = _text.transform
                .DOMoveX(-goal, _duration)
                .SetRelative()
                .SetDelay(_lifeTime)
                .OnKill(() => _tween = null);
        }

        public void MoveTween(float goal)
        {
            if (!IsAssigned) return;

            _text.transform.DOMoveY(goal, _duration);
        }

        public void ExitTween(float goal)
        {
            if (!IsAssigned) return;
            _tween?.Kill();

            _text.transform.DOMoveX(goal, _duration).SetRelative();
        }
    }
}
