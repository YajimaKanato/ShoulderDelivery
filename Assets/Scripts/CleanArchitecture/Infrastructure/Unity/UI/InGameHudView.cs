using ShoulderDelivery.Adapter;
using ShoulderDelivery.Common;
using System;
using TMPro;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class InGameHudView : MonoBehaviour, IGameHudView, IStageInfoView
    {
        [SerializeField] TextMeshProUGUI _timerText;
        [SerializeField] TextMeshProUGUI _scoreText;
        [SerializeField] TextMeshProUGUI _deliveryCountText;
        [SerializeField] TextMeshProUGUI _deliveryComboText;

        const string SCORE = "Score : ";
        const string DELIVERY = "Delivery : ";
        const string COMBO = " Combo";

        public event Action<StageHour> OnStart;

        /// <summary>参照がそろっているかを確認するためのプロパティ</summary>
        bool IsAssigned => _timerText != null
            && _scoreText != null
            && _deliveryCountText != null;

        private void Awake()
        {
            if (_timerText == null)
                UILogger.LogNotAssigned(_timerText);

            if (_scoreText == null)
                UILogger.LogNotAssigned(_scoreText);

            if (_deliveryCountText == null)
                UILogger.LogNotAssigned(_deliveryCountText);

            if (_deliveryComboText == null)
                UILogger.LogNotAssigned(_deliveryComboText);
        }

        public void ShowHud(HudViewModel viewModel)
        {
            if (!IsAssigned) return;

            var score = viewModel.Score;
            var remainingTime = Mathf.CeilToInt(viewModel.RemainingTime);
            var minutes = remainingTime / 60;
            var seconds = remainingTime % 60;

            _timerText.text = $"{minutes.ToString("0")}:{seconds.ToString("00")}";
            _scoreText.text = SCORE + score.ToString("00000");
        }

        public void ShowStageInfo(StageInfoViewModel viewModel)
        {
            if (!IsAssigned) return;

            var stageHour = viewModel.StageHour;
            var requiredDeliveryCount = viewModel.RequiredDeliveryCount;
            var timeLimitSeconds = viewModel.TimeLimitSeconds;
            var minutes = timeLimitSeconds / 60;
            var seconds = timeLimitSeconds % 60;

            _timerText.text = $"{minutes.ToString("0")}:{seconds.ToString("00")}";
            OnStart?.Invoke(stageHour);
            ShowDelivery(new DeliveryViewModel(0, 0, 0, requiredDeliveryCount));
        }

        public void ShowDelivery(DeliveryViewModel viewModel)
        {
            if (!IsAssigned) return;

            var score = viewModel.Score;
            var combo = viewModel.Combo;
            var deliveryCount = viewModel.DeliveryCount;
            var requiredCount = viewModel.RequiredCount;

            _deliveryCountText.text = DELIVERY
                + deliveryCount.ToString("0")
                + " / "
                + requiredCount.ToString("0");
            _scoreText.text = SCORE + score.ToString("00000");
            if (_deliveryComboText != null)
                _deliveryComboText.text = combo.ToString("0") + COMBO;
        }
    }
}
