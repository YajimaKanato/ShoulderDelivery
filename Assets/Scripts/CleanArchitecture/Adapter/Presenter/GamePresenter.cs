using ShoulderDelivery.UseCase;
using System;

namespace ShoulderDelivery.Adapter
{
    public class GamePresenter : IGameOutputPort
    {
        IStageInfoView _stageInfoView;
        IGameCountDownView _gameCountDownView;
        IGameHudView _hudView;
        IGameResultView _resultView;
        IDeliveriedView _deliveryView;
        ICardboardContextView _cardboardContextView;

        public GamePresenter(IStageInfoView stageInfoView
            , IGameCountDownView gameCountDownView
            , IGameHudView hudView
            , IGameResultView resultView
            , IDeliveriedView deliveryView
            , ICardboardContextView cardboardContextView)
        {
            if (stageInfoView == null)
                throw new ArgumentNullException(nameof(stageInfoView));

            if (gameCountDownView == null)
                throw new ArgumentNullException(nameof(gameCountDownView));

            if (hudView == null)
                throw new ArgumentNullException(nameof(hudView));

            if (resultView == null)
                throw new ArgumentNullException(nameof(resultView));

            if (deliveryView == null)
                throw new ArgumentNullException(nameof(deliveryView));

            if (cardboardContextView == null)
                throw new ArgumentNullException(nameof(cardboardContextView));

            _stageInfoView = stageInfoView;
            _gameCountDownView = gameCountDownView;
            _hudView = hudView;
            _resultView = resultView;
            _deliveryView = deliveryView;
            _cardboardContextView = cardboardContextView;
        }

        public void ChangeControllerEnable(bool enable)
        {
            // 操作権限を変更する
        }

        public void ShowCountDown(CountDownOutput output)
        {
            var remainingSeconds = output.Seconds;

            // ViewModel作成
            var viewModel = new CountDownViewModel(remainingSeconds);

            _gameCountDownView.ShowCountDown(viewModel);
        }

        public void ShowHud(GameHudOutput output)
        {
            var remainingTime = output.RemainingTime;
            var remainingDeliveryCount = output.RemainingDeliveryCount;
            var score = output.Score.ToString("0");

            // ViewModel作成
            var viewModel = new HudViewModel(remainingTime, remainingDeliveryCount, score);

            _hudView.ShowHud(viewModel);
        }

        public void ShowStageInfo(GameStartOutput output)
        {
            var timeLimitSeconds = output.TimeLimitSeconds;
            var requiredDeliveryCount = output.RequiredDeliveryCount;

            // ViewModel作成
            var viewModel = new StageInfoViewModel(timeLimitSeconds, requiredDeliveryCount);

            _stageInfoView.ShowStageInfo(viewModel);
        }

        public void ShowGameClear(GameClearOutput output)
        {
            var total = output.Total;
            var deliveryCount = output.DeliveryCount;
            var remainingTime = output.RemainingTime;

            //var viewModel
        }

        public void ShowGameFailed(GameFailedOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void ShowDeliverySucceeded(DeliverySuccessOutput deliveryResult)
        {
            var scoreBreakDown = deliveryResult.ScoreBreakdown;
            var score = deliveryResult.Score;

            // ViewModel作成
            var viewModel = new DeliveriedViewModel(score, scoreBreakDown.Total);

            _deliveryView.ShowDeliverySucceededResult(viewModel);
        }

        public void ShowDeliveryFailed(DeliveryFailedOutput deliveryResult)
        {
            // ViewModel作成
            var viewModel = new DeliveriedViewModel();

            _deliveryView.ShowDeliveryFailedResult(viewModel);
        }

        public void ShowThrowCardboardRejected(ThrowCardboardRejectedOutput output)
        {
            var viewModel = new CardboardContextViewModel("段ボールを投げることができませんでした");

            _cardboardContextView.ShowCardboardRejected(viewModel);
        }

        public void ShowThrowCardboardAccepted(ThrowCardboardAcceptOutput output)
        {
            var cardboardId = output.CardboardId;

            var viewModel = new CardboardContextViewModel($"段ボールを投げることができました\n{cardboardId}");

            _cardboardContextView.ShowCardboardAccepted(viewModel);
        }
    }
}
