using DG.Tweening;
using ShoulderDelivery.Adapter;
using TMPro;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class InGameCountDownView : MonoBehaviour, IGameCountDownView
    {
        [SerializeField] TextMeshProUGUI _countDownText;

        Tween _countDownTween = null;

        /// <summary>参照がそろっているかを確認するためのプロパティ</summary>
        bool IsAssignedUI => _countDownText != null;

        private void Awake()
        {
            if (_countDownText == null)
                UILogger.LogNotAssigned(_countDownText);
        }

        public void ShowCountDown(CountDownViewModel viewModel)
        {
            if (!IsAssignedUI) return;
            if (_countDownTween == null || _countDownTween.IsActive()) return;

            var seconds = viewModel.RemainingSeconds.ToString("0");
            _countDownText.text = seconds;

            var sequence = DOTween.Sequence();
            sequence.Append(_countDownText.transform.DOScale(0, 1).SetEase(Ease.InExpo));

            _countDownTween = sequence;
        }

        public void ShowGameStart(GameStartViewModel viewModel)
        {
            if (!IsAssignedUI) return;
            if (_countDownTween == null || _countDownTween.IsActive()) return;

            var text = "Let's Delivery!!";
            _countDownText.text = text;

            var sequence = DOTween.Sequence();
            sequence.Append(_countDownText.transform.DOScale(0, 2).SetEase(Ease.InQuad));

            _countDownTween = sequence;
        }
    }
}
