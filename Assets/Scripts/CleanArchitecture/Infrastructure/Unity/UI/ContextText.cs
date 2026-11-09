using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>ログを表示するクラス</summary>
    public class ContextText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _text;
        [SerializeField] RectTransform _rectTransform;
        [SerializeField] float _lifeTime = 1f;
        [SerializeField] float _duration = 0.3f;
        Sequence _sequence;
        Tween _moveTween;
        event Action<ContextText> _onExit;
        public Action OnExit(Action<ContextText> act)
        {
            _onExit += act;
            return () => _onExit -= act;
        }

        bool IsAssigned => _text != null;
        public bool IsShowing { get; private set; }

        private void Awake()
        {
            if (_text == null)
                UILogger.LogNotAssigned(_text);
        }

        /// <summary>
        /// 文字が画面内に移動するモーションを実行するメソッド
        /// </summary>
        /// <param name="entryPosX">X軸の画面内の座標</param>
        /// <param name="exitPosX">X軸の画面外の座標</param>
        /// <param name="startPos">演出開始位置</param>
        /// <param name="color">文字の色</param>
        /// <param name="message">表示するログ</param>
        public void EntryTween(float entryPosX, float exitPosX, float startPos, Color color, string message = null)
        {
            if (!IsAssigned) return;
            _sequence?.Kill();
            _moveTween?.Kill();

            // テキスト設定
            _text.text = message;
            // フラグ立て
            IsShowing = true;

            // アニメーション開始設定
            _rectTransform.anchoredPosition = new Vector3(exitPosX, startPos);
            _text.color = color;
            _text.alpha = 0;

            _sequence = DOTween.Sequence()
                .Append(_rectTransform.DOAnchorPosX(entryPosX, _duration))
                .Join(_text.DOFade(1, _duration))
                .AppendInterval(_lifeTime)
                .Append(_rectTransform.DOAnchorPosX(exitPosX, _duration))
                .Join(_text.DOFade(0, _duration))
                .OnComplete(CompleteExit);
        }

        /// <summary>
        /// 新たにテキストが画面内に入ったときに移動するメソッド
        /// </summary>
        /// <param name="goal">ずれる位置</param>
        public void MoveTween(float goal)
        {
            if (!IsAssigned) return;

            _moveTween?.Kill();
            _moveTween = _rectTransform.DOAnchorPosY(goal, _duration)
                .OnKill(() => _moveTween = null);
        }

        /// <summary>
        /// 画面外に出るモーションを実行するメソッド
        /// </summary>
        /// <param name="exitPosX">画面外の位置</param>
        public void ExitTween(float exitPosX)
        {
            if (!IsAssigned) return;
            _sequence?.Kill();
            _moveTween?.Kill();

            _sequence = DOTween.Sequence()
                .Append(_rectTransform.DOAnchorPosX(exitPosX, _duration))
                .Join(_text.DOFade(0, _duration))
                .OnComplete(CompleteExit);
        }

        /// <summary>
        /// 退場が完了したログを再利用できるようにする
        /// </summary>
        void CompleteExit()
        {
            _sequence = null;
            _moveTween?.Kill();
            IsShowing = false;
            _onExit?.Invoke(this);
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
            _moveTween?.Kill();
        }
    }
}
