namespace ShoulderDelivery.Entity
{
    /// <summary>配達結果を生成する静的クラス</summary>
    public static class DeliveryResultService
    {
        /// <summary>
        /// 配達成功の投擲結果を生成するメソッド
        /// </summary>
        /// <param name="targetDefinition">当たったターゲットの情報</param>
        /// <param name="combo">連続配達成功回数</param>
        /// <returns>配達成功の投擲結果</returns>
        public static DeliveryResult Delivered(TargetDefinition targetDefinition, int combo)
        {
            return new DeliveryResult(ThrowOutcome.Delivered, targetDefinition, combo);
        }

        /// <summary>
        /// 配達失敗の投擲結果を生成するメソッド
        /// </summary>
        /// <returns>配達失敗の投擲結果</returns>
        public static DeliveryResult Missed()
        {
            return new DeliveryResult(ThrowOutcome.Missed, null, 0);
        }
    }
}
