using ShoulderDelivery.Entity;
using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームの進行を管理するUseCaseクラス</summary>
    public sealed class GameUseCase
    {
        UseCaseReferences _references;

        public GameUseCase(UseCaseReferences references)
        {
            _references = references ?? throw new ArgumentNullException(nameof(references));
        }

        /// <summary>
        /// ゲームを開始するメソッド
        /// </summary>
        /// <param name="input">開始に必要なデータ</param>
        public void Start(GameStartInput input)
        {
            // ステージの情報を取得
            var stageDefinition = _references.StageRepository.Get(input.StageId);

            if (stageDefinition == null)
                throw new InvalidOperationException(nameof(stageDefinition));

            // ゲームの情報を生成して登録
            var gameSession = new GameSession(stageDefinition);
            _references.GameSessionStore.Set(gameSession);

            // ゲーム開始を通知
            _references.OutputPort.ShowStageInfo(
                new StageInfoOutput(stageDefinition.TimeLimitSeconds
                , stageDefinition.RequiredDeliveryCount));
        }

        /// <summary>
        /// ゲームを毎フレーム更新するメソッド
        /// </summary>
        /// <param name="input">更新に必要なデータ</param>
        /// <exception cref="InvalidOperationException">必要な参照がない</exception>
        public void Execute(TickInput input)
        {
            // 現在の時間の進行状況によって処理を変える
            var result = _references.StageState.CurrentPhase switch
            {
                StagePhase.CountDown => CountDown(input),
                StagePhase.IsPlaying => Tick(input),
                _ => StageTickResult.None
            };

            // 時間の進行結果に応じて処理を変える
            switch (result)
            {
                case StageTickResult.CountDownFinished:
                    CountDownFinished();
                    break;
                case StageTickResult.TimeUp:
                    // ゲームを終了する
                    FinishGame();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// カウントダウン情報を更新するメソッド
        /// </summary>
        /// <param name="input">チック情報</param>
        /// <param name="stageState">ステージ情報</param>
        /// <returns>ステージのチック結果</returns>
        StageTickResult CountDown(TickInput input)
        {
            var stageState = _references.StageState;

            // カウントダウンを進める
            var result = stageState.CountDown(input.Delta);

            // カウントダウンを表示
            _references.OutputPort.ShowCountDown(new CountDownOutput(stageState.RemainingCountDownSeconds));

            return result;
        }

        /// <summary>
        /// 毎フレーム情報を更新するメソッド
        /// </summary>
        /// <param name="session">ゲームのセッション情報</param>
        /// <param name="stageState">ステージの情報</param>
        /// <exception cref="InvalidOperationException">必要な参照がない</exception>
        /// <returns>ステージのチック結果</returns>
        StageTickResult Tick(TickInput input)
        {
            // ノルマクリアしていたら終了
            if (_references.DeliveryState.IsQuotaMet)
            {
                FinishGame();
                return StageTickResult.TimeUp;
            }

            var stageState = _references.StageState;

            var result = stageState.Tick(input.Delta);

            // 情報更新
            _references.OutputPort.ShowHud(new GameHudOutput(stageState.RemainingTime, _references.Score.Total));

            return result;
        }

        /// <summary>
        /// カウントダウン終了時のイベントメソッド
        /// </summary>
        void CountDownFinished()
        {
            _references.OutputPort.ShowGameStart(new GameStartOutput());
            _references.OutputPort.ChangeControllerEnable(true);
        }

        /// <summary>
        /// ゲームを終了するメソッド
        /// </summary>
        /// <exception cref="InvalidOperationException">必要な参照がない</exception>
        void FinishGame()
        {
            var score = _references.Score;
            var scoreRules = _references.ScoreRules;
            var stageState = _references.StageState;
            var stageDefinition = _references.StageDefinition;
            var deliveryState = _references.DeliveryState;

            // ゲームを終了状態にする
            if (!_references.StageState.Finish()) return;

            // 残り時間ボーナスを計算
            var timeBonus = ScoreCalculator.CalculateRemainingSecondsScore(stageState, scoreRules);

            // スコアを更新
            _references.Score.AddScore(timeBonus);

            // 結果を表示
            if (deliveryState.IsQuotaMet)
            {
                _references.OutputPort.ShowGameClear(new GameClearOutput(score.Total
                    , deliveryState.RequiredDeliveryCount
                    , deliveryState.DeliveredCount
                    , stageDefinition.TimeLimitSeconds
                    , stageState.RemainingTime));
            }
            else
            {
                _references.OutputPort.ShowGameFailed(new GameFailedOutput(score.Total
                    , deliveryState.RequiredDeliveryCount
                    , deliveryState.DeliveredCount
                    , stageDefinition.TimeLimitSeconds
                    , stageState.RemainingTime));
            }
        }
    }
}
