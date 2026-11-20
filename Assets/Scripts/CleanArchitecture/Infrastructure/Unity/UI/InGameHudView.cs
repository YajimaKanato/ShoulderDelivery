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
        [SerializeField, Min(0)] float _bonusHoldDuration = 0.6f;
        [SerializeField, Min(0.01f)] float _bonusMoveDuration = 0.6f;
        [SerializeField, Min(0)] float _bonusMoveDistance = 40f;

        TextMeshProUGUI _bonusText;
        Sequence _bonusSequence;
        int _actualScore;
        int _pendingBonus;

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
            ShowScore(score);
        }

        public void ShowStageInfo(StageInfoViewModel viewModel)
        {
            if (!IsAssigned) return;

            var stageHour = viewModel.StageHour;
            var requiredDeliveryCount = viewModel.RequiredDeliveryCount;
            StopDeliveryBonus();
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
            ShowScore(score);
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

        public void ShowDeliveryBonus(DeliveriedViewModel viewModel)
        {
            var bonus = viewModel.SpeedBonus + viewModel.DistanceBonus;
            if (!IsAssigned || bonus <= 0) return;

            StopDeliveryBonus();
            if (_bonusText == null)
            {
                // スコア表示の書体を使い、加算演出専用のテキストを作る
                _bonusText = Instantiate(_scoreText, _scoreText.rectTransform, false);
                _bonusText.name = "DeliveryBonusText";
                _bonusText.raycastTarget = false;
                _bonusText.enableAutoSizing = false;
                _bonusText.fontSize = _scoreText.fontSize * 0.6f;
                var rect = _bonusText.rectTransform;
                rect.anchorMin = new Vector2(0, 1);
                rect.anchorMax = new Vector2(1, 1);
                rect.pivot = new Vector2(0.5f, 0);
                rect.sizeDelta = new Vector2(0, _bonusText.fontSize * 1.25f);
            }

            var speed = viewModel.SpeedBonus > 0 ? $"速度 +{viewModel.SpeedBonus}" : "";
            var distance = viewModel.DistanceBonus > 0 ? $"距離 +{viewModel.DistanceBonus}" : "";
            var separator = speed.Length > 0 && distance.Length > 0 ? "  " : "";
            _bonusText.text = speed + separator + distance;
            _bonusText.rectTransform.anchoredPosition = Vector2.zero;
            _bonusText.alpha = _scoreText.alpha;
            _bonusText.gameObject.SetActive(true);
            _pendingBonus = bonus;
            ShowScore(viewModel.TotalScore);

            _bonusSequence = DOTween.Sequence().SetUpdate(true)
                .AppendInterval(_bonusHoldDuration)
                .Append(_bonusText.rectTransform.DOAnchorPosY(-_bonusMoveDistance, _bonusMoveDuration).SetEase(Ease.Linear))
                .Join(_bonusText.DOFade(0, _bonusMoveDuration).SetEase(Ease.Linear))
                .Join(DOTween.To(() => (float)bonus, value =>
                {
                    _pendingBonus = Mathf.RoundToInt(value);
                    ShowScore(_actualScore);
                }, 0, _bonusMoveDuration).SetEase(Ease.Linear))
                .OnComplete(() =>
                {
                    _bonusSequence = null;
                    StopDeliveryBonus();
                });
        }

        void ShowScore(int score)
        {
            _actualScore = score;
            _scoreText.text = SCORE + Mathf.Max(0, score - _pendingBonus).ToString("00000");
        }

        void StopDeliveryBonus()
        {
            _bonusSequence?.Kill();
            _bonusSequence = null;
            _pendingBonus = 0;
            if (_bonusText != null) _bonusText.gameObject.SetActive(false);
            if (_scoreText != null) ShowScore(_actualScore);
        }

        private void OnDisable() => StopDeliveryBonus();
    }
}
