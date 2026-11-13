using ShoulderDelivery.Entity;
using ShoulderDelivery.UseCase;

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
        ITargetView _targetView;
        IControllerView _controllerView;
        ICursorController _cursorController;

        public GamePresenter(IStageInfoView stageInfoView
            , IGameCountDownView gameCountDownView
            , IGameHudView hudView
            , IGameResultView resultView
            , IDeliveriedView deliveryView
            , ICardboardContextView cardboardContextView
            , ITargetView targetView
            , IControllerView controllerView
            , ICursorController cursorController)
        {
            _stageInfoView = stageInfoView;
            _gameCountDownView = gameCountDownView;
            _hudView = hudView;
            _resultView = resultView;
            _deliveryView = deliveryView;
            _cardboardContextView = cardboardContextView;
            _targetView = targetView;
            _controllerView = controllerView;
            _cursorController = cursorController;
        }

        public void ChangeControllerEnable(bool enable)
        {
            _controllerView?.SetControllerEnabled(enable);
        }

        public void ShowCountDown(CountDownOutput output)
        {
            var remainingSeconds = output.Seconds;

            // ViewModel作成
            var viewModel = new CountDownViewModel(remainingSeconds);

            _gameCountDownView?.ShowCountDown(viewModel);
        }

        public void ShowHud(GameHudOutput output)
        {
            var remainingTime = output.RemainingTime;
            var score = output.Score;

            // ViewModel作成
            var viewModel = new HudViewModel(remainingTime, score);

            _hudView?.ShowHud(viewModel);
        }

        public void ShowStageInfo(StageInfoOutput output)
        {
            var stageHour = output.StageHour;
            var timeLimitSeconds = output.TimeLimitSeconds;
            var requiredDeliveryCount = output.RequiredDeliveryCount;

            // ViewModel作成
            var viewModel = new StageInfoViewModel(stageHour, timeLimitSeconds, requiredDeliveryCount);

            _stageInfoView?.ShowStageInfo(viewModel);
        }

        public void ShowGameClear(GameClearOutput output)
        {
            _targetView?.ClearTarget();
            var total = output.TotalScore;
            var requiredDeliveryCount = output.RequiredDeliveryCount;
            var deliveryCount = output.DeliveryCount;
            var clearTime = output.TimeLimitSeconds - output.RemainingTime;

            // ViewModel作成
            var viewModel = new GameClearViewModel(total, requiredDeliveryCount, deliveryCount, clearTime);

            _resultView?.ShowClear(viewModel);
        }

        public void ShowGameFailed(GameFailedOutput output)
        {
            _targetView?.ClearTarget();
            var total = output.TotalScore;
            var requiredDeliveryCount = output.RequiredDeliveryCount;
            var deliveryCount = output.DeliveryCount;
            var clearTime = output.TimeLimitSeconds - output.RemainingTime;

            // ViewModel作成
            var viewModel = new GameFailedViewModel(total, requiredDeliveryCount, deliveryCount, clearTime);

            _resultView?.ShowFailed(viewModel);
        }

        public void ShowDeliverySucceeded(DeliverySuccessOutput deliveryResult)
        {
            var scoreBreakDown = deliveryResult.ScoreBreakdown;
            var score = deliveryResult.Score;

            // ViewModel作成
            var viewModel = new DeliveriedViewModel(score, scoreBreakDown.Total);

            _deliveryView?.ShowDeliverySucceededResult(viewModel);
            ShowDelivery(deliveryResult.Score, deliveryResult.DeliveryBreakdown);
        }

        public void ShowDeliveryFailed(DeliveryFailedOutput deliveryResult)
        {
            // ViewModel作成
            var viewModel = new DeliveriedViewModel();

            _deliveryView?.ShowDeliveryFailedResult(viewModel);
            ShowDelivery(deliveryResult.Score, deliveryResult.DeliveryBreakdown);
        }

        /// <summary>
        /// 配達後のスコアと配達状況をHUDに表示するメソッド
        /// </summary>
        /// <param name="score">現在のスコア</param>
        /// <param name="deliveryBreakdown">配達の内訳</param>
        void ShowDelivery(int score, DeliveryBreakdown deliveryBreakdown)
        {
            var viewModel = new DeliveryViewModel(score
                , deliveryBreakdown.DeliveryCombo
                , deliveryBreakdown.DeliveryCount
                , deliveryBreakdown.RequiredDeliveryCount);

            _hudView?.ShowDelivery(viewModel);
        }

        public void ShowThrowCardboardRejected(ThrowCardboardRejectedOutput output)
        {
            // ViewModel作成
            var viewModel = new CardboardContextViewModel("Cannot throw yet");

            _cardboardContextView?.ShowCardboardRejected(viewModel);
        }

        public void ShowThrowCardboardAccepted(ThrowCardboardAcceptOutput output)
        {
            // ViewModel作成
            var viewModel = new CardboardContextViewModel("Package thrown");

            _cardboardContextView?.ShowCardboardAccepted(viewModel);
        }

        public void ShowGameStart(GameStartOutput output)
        {
            var viewModel = new GameStartViewModel();

            _gameCountDownView?.ShowGameStart(viewModel);
        }

        public void ShowNextDelivery(NextDeliveryTargetOutput output)
        {
            var viewModel = new NextTargetViewModel(output.TargetId);

            _targetView?.UpdateTarget(viewModel);
        }

        public void ChangeCursorVisible(bool visible)
        {
            if (visible)
            {
                _cursorController?.ShowCursor();
            }
            else
            {
                _cursorController?.HideCursor();
            }
        }
    }
}
