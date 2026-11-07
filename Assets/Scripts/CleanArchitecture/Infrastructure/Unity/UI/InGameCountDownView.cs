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
            _countDownText.text = Mathf.Max(1, Mathf.CeilToInt(viewModel.RemainingSeconds)).ToString();
        }

        public void ShowGameStart(GameStartViewModel viewModel)
        {
            if (!IsAssigned) return;

            if (_countDownTween != null && _countDownTween.IsActive())
            {
                _countDownTween.Kill();
                _countDownTween = null;
            }

            _countDownText.text = "START!";
            _countDownText.transform.localScale = Vector3.one;

            var sequence = DOTween.Sequence();
            sequence.Append(_countDownText.transform.DOScale(0, _startAnnouncementLifeTime).SetEase(Ease.InQuad));

            _countDownTween = sequence;
        }

        private void OnDestroy() => _countDownTween?.Kill();
    }
}
