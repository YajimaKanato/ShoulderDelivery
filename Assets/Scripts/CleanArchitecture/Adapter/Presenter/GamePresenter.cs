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
            var score = output.Score;

            // ViewModel作成
            var viewModel = new HudViewModel(remainingTime, score);

            _hudView.ShowHud(viewModel);
        }

        public void ShowStageInfo(StageInfoOutput output)
        {
            var timeLimitSeconds = output.TimeLimitSeconds;
            var requiredDeliveryCount = output.RequiredDeliveryCount;

            // ViewModel作成
            var viewModel = new StageInfoViewModel(timeLimitSeconds, requiredDeliveryCount);

            _stageInfoView.ShowStageInfo(viewModel);
        }

        public void ShowGameClear(GameClearOutput output)
        {
            var total = output.TotalScore;
            var requiredDeliveryCount = output.RequiredDeliveryCount;
            var deliveryCount = output.DeliveryCount;
            var clearTime = output.TimeLimitSeconds - output.RemainingTime;

            // ViewModel作成
            var viewModel = new GameClearViewModel(total, requiredDeliveryCount, deliveryCount, clearTime);

            _resultView.ShowClear(viewModel);
        }

        public void ShowGameFailed(GameFailedOutput output)
        {
            var total = output.TotalScore;
            var requiredDeliveryCount = output.RequiredDeliveryCount;
            var deliveryCount = output.DeliveryCount;
            var clearTime = output.TimeLimitSeconds - output.RemainingTime;

            // ViewModel作成
            var viewModel = new GameFailedViewModel(total, requiredDeliveryCount, deliveryCount, clearTime);

            _resultView.ShowFailed(viewModel);
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
            // ViewModel作成
            var viewModel = new CardboardContextViewModel("段ボールを投げることができませんでした");

            _cardboardContextView.ShowCardboardRejected(viewModel);
        }

        public void ShowThrowCardboardAccepted(ThrowCardboardAcceptOutput output)
        {
            var cardboardId = output.CardboardId;

            // ViewModel作成
            var viewModel = new CardboardContextViewModel($"段ボールを投げることができました\n{cardboardId}");

            _cardboardContextView.ShowCardboardAccepted(viewModel);
        }

        public void ShowGameStart(GameStartOutput output)
        {
            var viewModel = new GameStartViewModel();

            _gameCountDownView.ShowGameStart(viewModel);
        }
    }
}
