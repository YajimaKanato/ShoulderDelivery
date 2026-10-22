using ShoulderDelivery.Entity;
using System;

namespace ShoulderDelivery.UseCase
{
    /// <summary>段ボールの結果を管理するUseCaseクラス</summary>
    public sealed class ResolveDeliveryUseCase : IDeliveryUseCase
    {
        UseCaseReferences _references;

        public ResolveDeliveryUseCase(UseCaseReferences references)
        {
            _references = references ?? throw new ArgumentNullException(nameof(references));
        }

        public bool TryDelivery(TargetId? targetId, ThrowContext context)
        {
            // 配達をする
            if (targetId == null || !_references.DeliveryState.TryDelivery(targetId.Value))
            {
                // 配達失敗
                _references.OutputPort.ShowDeliveryFailed(DeliveryResultOutputService.Missed());
                return false;
            }

            UpdateTarget();

            // ターゲットの情報を取得
            var targetDefinition = _references.TargetRepository.Get(targetId.Value);

            // 配達成功情報を取得
            var deliveryResult = DeliveryResultService.Delivered(targetDefinition
                , _references.DeliveryState.DeliveryCombo);

            // スコアを生成
            var scoreBreakdown = ScoreCalculator.CalculateDeliveryScore(context
                , deliveryResult
                , _references.ScoreRules);

            // スコアを更新
            _references.Score.AddScore(scoreBreakdown);

            // 配達成功を通知
            _references.OutputPort.ShowDeliverySucceeded(DeliveryResultOutputService.Delivered(scoreBreakdown
                , _references.Score.Total
                , DeliveryBreakdownService.Deliveried(_references.DeliveryState)));

            return true;
        }

        /// <summary>
        /// ターゲットを更新するメソッド
        /// </summary>
        /// <exception cref="InvalidOperationException">参照がない</exception>
        internal void UpdateTarget()
        {
            // ターゲットの更新
            _references.DeliveryState.NextTarget();
        }
    }
}
