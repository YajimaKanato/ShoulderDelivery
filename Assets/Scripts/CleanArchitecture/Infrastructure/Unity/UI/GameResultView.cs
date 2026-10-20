using ShoulderDelivery.Adapter;
using TMPro;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class GameResultView : MonoBehaviour, IGameResultView
    {
        [SerializeField] TextMeshProUGUI _scoreText;
        [SerializeField] TextMeshProUGUI _timeText;
        [SerializeField] TextMeshProUGUI _deliveryCountText;
        [SerializeField] TextMeshProUGUI _resultText;
        bool IsAssigned => _resultText != null
            && _scoreText != null
            && _timeText != null
            && _deliveryCountText != null;

        public void ShowClear(GameClearViewModel viewModel)
        {
            if (!IsAssigned) return;

            var score = viewModel.TotalScore.ToString("00000");
            var minute = viewModel.ClearTime / 60;
            var second = viewModel.ClearTime % 60;
            var time = $"{minute.ToString("00")} : {second.ToString("00")}";
            var deliveryCount = $"{viewModel.DeliveryCount} / {viewModel.RequiredDeliveryCount}";

            _scoreText.text = score;
            _timeText.text = time;
            _deliveryCountText.text = deliveryCount;
            _resultText.text = "ステージクリア成功！！";
        }

        public void ShowFailed(GameFailedViewModel viewModel)
        {
            if (!IsAssigned) return;

            var score = viewModel.TotalScore.ToString("00000");
            var minute = viewModel.ClearTime / 60;
            var second = viewModel.ClearTime % 60;
            var time = $"{minute.ToString("00")} : {second.ToString("00")}";
            var deliveryCount = $"{viewModel.DeliveryCount} / {viewModel.RequiredDeliveryCount}";

            _scoreText.text = score;
            _timeText.text = time;
            _deliveryCountText.text = deliveryCount;
            _resultText.text = "ステージクリア失敗…";
        }
    }
}
