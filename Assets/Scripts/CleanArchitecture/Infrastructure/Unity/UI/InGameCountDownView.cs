using ShoulderDelivery.Adapter;
using TMPro;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class InGameCountDownView : MonoBehaviour, IGameCountDownView
    {
        [SerializeField] TextMeshProUGUI _countDownText;

        /// <summary>参照がそろっているかを確認するためのプロパティ</summary>
        bool IsAssignedUI => _countDownText != null;

        public void ShowCountDown(CountDownViewModel viewModel)
        {

        }
    }
}
