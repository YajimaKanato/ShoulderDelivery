using ShoulderDelivery.UseCase;
using UnityEngine;

namespace ShoulderDelivery.Adapter
{
    public class GamePresenter : MonoBehaviour, IGameOutputPort
    {
        [SerializeField] GameHudView _hudView;
        [SerializeField] GameResultView _resultView;
        [SerializeField] DeliveriedView _deliveryView;
        [SerializeField] CardboardContextView _cardboardContextView;

        public void ChangeControllerEnable(bool enable)
        {
            throw new System.NotImplementedException();
        }

        public void ShowHud(GameHudOutput output)
        {
            if (_hudView == null)
            {
                Debug.LogWarning("GameHudViewが設定されていません", _hudView);
                return;
            }

            var remainingTime = output.RemainingTime.ToString("0.0");
            var remainingDeliveryCount = output.RemainingDeliveryCount;
            var score = output.Score;
        }

        public void ShowHud(StartGameOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void ShowGameClear(GameClearOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void ShowGameFailed(GameFailedOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void ShowDeliverySucceeded(DeliverySuccessOutput deliveryResult)
        {
            throw new System.NotImplementedException();
        }

        public void ShowDeliveryFailed(DeliveryFailedOutput deliveryResult)
        {
            throw new System.NotImplementedException();
        }

        public void ShowThrowCardboardRejected(ThrowCardboardRejectedOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void ShowThrowCardboardAccepted(ThrowCardboardAcceptOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
