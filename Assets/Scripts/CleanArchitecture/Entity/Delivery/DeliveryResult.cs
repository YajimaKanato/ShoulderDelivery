namespace ShoulderDelivery.Entity
{
    /// <summary>配達結果を保存する構造体</summary>
    public readonly struct DeliveryResult
    {
        public readonly TargetDefinition TargetDefinition;
        public readonly int Combo;

        public DeliveryResult(TargetDefinition targetDefinition, int combo)
        {
            TargetDefinition = targetDefinition;
            Combo = combo;
        }

        /// <summary>
        /// 配達成功の投擲結果を生成するメソッド
        /// </summary>
        /// <param name="targetDefinition">当たったターゲットの情報</param>
        /// <param name="combo">連続配達成功回数</param>
        /// <returns>配達成功の投擲結果</returns>
        public static DeliveryResult Delivered(TargetDefinition targetDefinition, int combo)
        {
            return new DeliveryResult(targetDefinition, combo);
        }
    }
}
