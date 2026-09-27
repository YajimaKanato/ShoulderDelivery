using DG.Tweening;
using ShoulderDelivery.Adapter;
using TMPro;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class InGameCountDownView : MonoBehaviour, IGameCountDownView
    {
        [SerializeField] TextMeshProUGUI _countDownText;
        [SerializeField] float _startAnnouncementLifeTime = 2f;

        Tween _countDownTween = null;

        /// <summary>参照がそろっているかを確認するためのプロパティ</summary>
        bool IsAssigned => _countDownText != null;

        private void Awake()
        {
            if (_countDownText == null)
                UILogger.LogNotAssigned(_countDownText);
        }

        public void ShowCountDown(CountDownViewModel viewModel)
        {
            if (!IsAssigned) return;
            if (_countDownTween == null || _countDownTween.IsActive()) return;

            var seconds = viewModel.RemainingSeconds.ToString("0");
            _countDownText.text = seconds;

            var sequence = DOTween.Sequence();
            sequence.Append(_countDownText.transform.DOScale(0, 1).SetEase(Ease.InExpo));

            _countDownTween = sequence;
        }

        public void ShowGameStart(GameStartViewModel viewModel)
        {
            if (!IsAssigned) return;

            if (_countDownTween != null && _countDownTween.IsActive())
            {
                _countDownTween.Kill();
                _countDownTween = null;
            }

            var text = "Let's Delivery!!";
            _countDownText.text = text;

            var sequence = DOTween.Sequence();
            sequence.Append(_countDownText.transform.DOScale(0, _startAnnouncementLifeTime).SetEase(Ease.InQuad));

            _countDownTween = sequence;
        }
    }
}
