using System;

namespace ShoulderDelivery.Entity
{
    public static class ScoreCalculator
    {
        /// <summary>
        /// スコア内訳を保存したデータを生成するメソッド
        /// </summary>
        /// <param name="throwContext">投擲時の情報</param>
        /// <param name="deliveryResult">配達結果の情報</param>
        /// <param name="rules">スコア計算上のルールオブジェクト</param>
        /// <returns>スコア内訳を保存したデータ</returns>
        //public static ScoreBreakdown CalculateDeliveryScore(ThrowContext? throwContext
        //    , DeliveryResult? deliveryResult
        //    , ScoreRules rules)
        //{
        //    if(throwContext == null)
        //        throw new ArgumentNullException(nameof(throwContext));

        //    if (deliveryResult == null)
        //        throw new ArgumentNullException(nameof(deliveryResult));

        //    if (rules == null)
        //        throw new ArgumentNullException(nameof(rules));

        //    // ValueObjectの実体を取得
        //    var throwContextValue = throwContext.Value;
        //    var deliveryResultValue = deliveryResult.Value;

        //    // 配達失敗時は影響のないスコアを返す
        //    if (deliveryResultValue.Outcome == ThrowOutcome.Missed)
        //        return new ScoreBreakdown(0, 0, 0, 0, 0);

        //    // ターゲットの情報を取得
        //    var targetDefinition = deliveryResultValue.TargetDefinition;

        //    if (targetDefinition == null)
        //        throw new InvalidOperationException(nameof(targetDefinition));

        //    var distance = Vector3.SqrMagnitude(throwContextValue.Position - targetDefinition.Value.Position);

        //}
    }
}
