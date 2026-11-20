using DG.Tweening;
using ShoulderDelivery.Adapter;
using ShoulderDelivery.Common;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShoulderDelivery.Infrastructure
{
    public class InGameHudView : MonoBehaviour, IGameHudView, IStageInfoView
    {
        [SerializeField] TextMeshProUGUI _timerText;
        [SerializeField] TextMeshProUGUI _scoreText;
        [SerializeField] TextMeshProUGUI _deliveryCountText;
        [SerializeField] TextMeshProUGUI _deliveryComboText;
        [SerializeField] ComboUI _combo;

        const string SCORE = "スコア : ";
        const string DELIVERY = "配達完了 : ";
        const string COMBO = " コンボ！";

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
            var remainingSeconds = StageTimeDisplay.GetRemainingSeconds(viewModel.RemainingTime);
            _timerText.text = StageTimeDisplay.Format(remainingSeconds);
            _scoreText.text = SCORE + score.ToString("00000");
        }

        public void ShowStageInfo(StageInfoViewModel viewModel)
        {
            if (!IsAssigned) return;

            var stageHour = viewModel.StageHour;
            var requiredDeliveryCount = viewModel.RequiredDeliveryCount;
            _combo?.Exit();
            _timerText.text = StageTimeDisplay.Format(viewModel.TimeLimitSeconds);
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
            {
                _combo?.Exit();
                if (combo > 0)
                {
                    _combo?.Entry();
                }
                _deliveryComboText.text = combo.ToString("0") + COMBO;
            }
        }
    }
}
