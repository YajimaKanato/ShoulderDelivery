using System;

namespace ShoulderDelivery.Entity
{
    /// <summary>スコアを計算するクラス</summary>
    public static class ScoreCalculator
    {
        /// <summary>
        /// スコア内訳を保存したデータを生成するメソッド
        /// </summary>
        /// <param name="throwContext">投擲時の情報</param>
        /// <param name="deliveryResult">配達結果の情報</param>
        /// <param name="rules">スコア計算上のルールオブジェクト</param>
        /// <returns>スコア内訳を保存したデータ</returns>
        public static ScoreBreakdown CalculateDeliveryScore(ThrowContext? throwContext
            , DeliveryResult? deliveryResult
            , ScoreRules rules)
        {
            if (throwContext == null)
                throw new ArgumentNullException(nameof(throwContext));

            if (deliveryResult == null)
                throw new ArgumentNullException(nameof(deliveryResult));

            if (rules == null)
                throw new ArgumentNullException(nameof(rules));

            // ValueObjectの実体を取得
            var throwContextValue = throwContext.Value;
            var deliveryResultValue = deliveryResult.Value;

            // 配達失敗時は影響のないスコアを返す
            if (deliveryResultValue.Outcome == ThrowOutcome.Missed)
                return new ScoreBreakdown(0, 0, 0, 0, 0);

            // ターゲットの情報を取得
            var targetDefinition = deliveryResultValue.TargetDefinition;

            if (targetDefinition == null)
                throw new InvalidOperationException(nameof(targetDefinition));

            // 距離に応じたボーナススコアを計算
            var distance = Vector3.SqrMagnitude(throwContextValue.Position - targetDefinition.Value.Position);
            var distanceBonus = rules.DistanceBonus.ResolveDistanceBonus(distance);

            // 連続配達成功回数に応じたボーナススコアを計算
            var deliveryComboBonus = rules.DeliveryComboBonus.ResolveDeliveryComboBonus(deliveryResultValue.Combo);

            // 移動速度に応じたボーナススコアを計算
            var speedBonus = rules.SpeedBonusBonus.ResolveSpeedBonus(throwContextValue.Speed);

            return new ScoreBreakdown(rules.DeliverySuccessScore, deliveryComboBonus, speedBonus, distanceBonus, 0);
        }

        public static ScoreBreakdown CalculateRemainingSecondsScore(StageState stageState
            , ScoreRules rules)
        {
            if (stageState == null)
                throw new ArgumentNullException(nameof(stageState));

            if (rules == null)
                throw new ArgumentNullException(nameof(rules));

            // 残り時間に応じたボーナススコアを計算
            var remainigTimeBonus = rules.RemainingTimeBonus.ResolveRemainigTimeBonus(stageState.RemainingTime);

            return new ScoreBreakdown(0, 0, 0, 0, remainigTimeBonus);
        }
    }
}
