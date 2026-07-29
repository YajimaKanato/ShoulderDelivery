namespace ShoulderDelivery.Entity
{
    /// <summary>投擲結果を生成する静的クラス</summary>
    public static class ThrowResultService
    {
        /// <summary>
        /// 配達成功の投擲結果を生成するメソッド
        /// </summary>
        /// <param name="id">ターゲットのID</param>
        /// <param name="position">投擲場所</param>
        /// <returns>配達成功の投擲結果</returns>
        public static ThrowResult Delivered(TargetId id, Vector3 position)
        {
            return new ThrowResult(ThrowOutcome.Delivered, id, position);
        }

        /// <summary>
        /// 配達失敗の投擲結果を生成するメソッド
        /// </summary>
        /// <returns>配達失敗の投擲結果</returns>
        public static ThrowResult Missed()
        {
            return new ThrowResult(ThrowOutcome.Missed, null, null);
        }
    }
}
