using ShoulderDelivery.Adapter;
using TMPro;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class InGameHudView : MonoBehaviour, IGameHudView, IStageInfoView
    {
        [SerializeField] TextMeshProUGUI _timerText;
        [SerializeField] TextMeshProUGUI _scoreText;
        [SerializeField] TextMeshProUGUI _requiredDeliveryCountText;

        /// <summary>参照がそろっているかを確認するためのプロパティ</summary>
        bool IsAssigned => _timerText != null
            && _scoreText != null
            && _requiredDeliveryCountText != null;

        private void Awake()
        {
            if (_timerText == null)
                UILogger.LogNotAssigned(_timerText);

            if (_scoreText == null)
                UILogger.LogNotAssigned(_scoreText);

            if (_requiredDeliveryCountText == null)
                UILogger.LogNotAssigned(_requiredDeliveryCountText);
        }

        public void ShowHud(HudViewModel viewModel)
        {
            if (!IsAssigned) return;

            var score = viewModel.Score;
            var remainingTime = viewModel.RemainingTime;
            var minutes = remainingTime / 60;
            var seconds = remainingTime % 60;

            _timerText.text = $"{minutes.ToString("0")}:{seconds.ToString("00")}";
            _scoreText.text = score.ToString("0");
        }

        public void ShowStageInfo(StageInfoViewModel viewModel)
        {
            if (!IsAssigned) return;

            var requiredDeliveryCount = viewModel.RequiredDeliveryCount;
            var timeLimitSeconds = viewModel.TimeLimitSeconds;
            var minutes = timeLimitSeconds / 60;
            var seconds = timeLimitSeconds % 60;

            _timerText.text = $"{minutes.ToString("0")}:{seconds.ToString("00")}";
            _requiredDeliveryCountText.text = requiredDeliveryCount.ToString("0");
        }
    }
}
