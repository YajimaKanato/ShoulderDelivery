using ShoulderDelivery.Entity;
using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>段ボール全般の処理の流れを持つクラス</summary>
    public class CardboardUseCase
    {
        UseCaseReferences _references;

        public CardboardUseCase(UseCaseReferences references)
        {
            _references = references ?? throw new ArgumentNullException(nameof(references));
        }

        /// <summary>
        /// 段ボールの投擲をするメソッド
        /// </summary>
        /// <param name="input">投擲時の情報</param>
        public void Throw(ThrowCardboardInput input)
        {
            // ゲームをプレイ中でなければ無視
            if (!_references.StageState.IsPlaying)
            {
                // 投擲失敗を通知
                _references.OutputPort.ShowThrowCardboardRejected(ThrowCardboardRejectedOutput.Rejected());
                return;
            }
            // ID生成
            var cardboardId = _references.CardboardIdGenerator.GenerateId();

            // 段ボールのIDと投擲時の情報を保存
            _references.InFlightCardboardState.RegisterThrowContext(cardboardId, input.Context);

            var targetId = _references.DeliveryState.CurrentTargetId;
            var target = _references.TargetRepository.Get(targetId);

            var cardboardWeight = target.RequestedWeight;

            // 投擲命令
            _references.Launcher.LaunchCardboard(ThrowCardboardOutput.Output(cardboardId, cardboardWeight, input));

            // 結果を通知
            _references.OutputPort.ShowThrowCardboardAccepted(ThrowCardboardAcceptOutput.Accepted(cardboardId));
        }

        /// <summary>
        /// 家に段ボールが当たったときに呼ばれるメソッド
        /// </summary>
        /// <param name="input">入力</param>
        public void HitTarget(ResolveDeliveryInput input)
        {
            // ゲームをプレイ中でなければ無視
            if (!_references.StageState.IsPlaying) return;

            // 投擲時の情報を取得する
            if (!_references.InFlightCardboardState.TryResolve(input.CardboardId, out var context))
                return;

            // 配達先のIDを取得
            var targetId = input.TargetId;

            // 配達をする
            if (targetId == null || !_references.DeliveryState.TryDelivery(targetId.Value))
                return;

            // 配達成功
            Delivery(targetId, context);
            DiscardCardboard(input.CardboardId);
        }

        /// <summary>
        /// 配達処理を行うメソッド
        /// </summary>
        /// <param name="targetId">配達先のID</param>
        /// <param name="context">投擲情報</param>
        void Delivery(TargetId? targetId, ThrowContext context)
        {
            // ターゲットの情報を取得
            var targetDefinition = _references.TargetRepository.Get(targetId.Value);

            // 配達成功情報を取得
            var deliveryResult = DeliveryResult.Delivered(targetDefinition
                , _references.DeliveryState.ResolveCombo(true));

            // スコアを生成
            var scoreBreakdown = ScoreCalculator.CalculateDeliveryScore(context
                , deliveryResult
                , _references.ScoreRules);

            // スコアを更新
            _references.Score.AddScore(scoreBreakdown);

            // 配達成功を通知
            _references.OutputPort.ShowDeliverySucceeded(DeliverySuccessOutput.Delivered(scoreBreakdown
                , _references.Score.Total
                , DeliveryBreakdown.Deliveried(_references.DeliveryState)));

            // ターゲット更新
            if (!_references.DeliveryState.IsQuotaMet)
                UpdateTarget();
        }

        /// <summary>
        /// ターゲットを更新するメソッド
        /// </summary>
        /// <exception cref="InvalidOperationException">参照がない</exception>
        void UpdateTarget()
        {
            // ターゲットの更新
            _references.DeliveryState.NextTarget();

            var targetId = _references.DeliveryState.CurrentTargetId;
            var target = _references.TargetRepository.Get(targetId);
            var targetCardboardWeight = target.RequestedWeight;

            // 次の投擲する段ボールの重さ
            _references.Launcher.SetNextThrowingCardboard(targetCardboardWeight);

            // 次の配達先を描画
            _references.OutputPort.ShowNextDelivery(NextDeliveryTargetOutput.GenerateOutput(targetId));
        }

        /// <summary>
        /// 段ボールが消えたことを処理するメソッド
        /// </summary>
        /// <param name="input">消えた段ボールの情報</param>
        /// <exception cref="InvalidOperationException">必要な参照がない</exception>
        public void Discard(DiscardCardboardInput input)
        {
            // 投擲情報のない段ボールやゲーム終了後の破棄は配達失敗にしない
            if (!DiscardCardboard(input.Id)
                || !_references.StageState.IsPlaying
                || _references.DeliveryState.IsQuotaMet) return;

            // コンボが途切れたことを通知
            var deliveryState = _references.DeliveryState;
            deliveryState.ResolveCombo(false);
            _references.OutputPort.ShowDeliveryFailed(DeliveryFailedOutput.Missed(_references.Score.Total
                , DeliveryBreakdown.Deliveried(deliveryState)));
        }

        /// <summary>
        /// 段ボールを消すメソッド
        /// </summary>
        /// <param name="id">消す段ボールのID</param>
        /// <returns>投擲情報を削除できたか</returns>
        bool DiscardCardboard(CardboardId id)
        {
            var discarded = _references.InFlightCardboardState.Discard(id);
            _references.DiscardCardboard.DiscardCardboard(id);
            return discarded;
        }
    }
}
