namespace ShoulderDelivery.UseCase
{
    /// <summary>投擲失敗結果を持つDTO</summary>
    public readonly struct ThrowCardboardRejectedOutput
    {
        /// <summary>
        /// 投擲失敗を生成する
        /// </summary>
        /// <returns>投擲失敗DTO</returns>
        internal static ThrowCardboardRejectedOutput Rejected()
        {
            return new ThrowCardboardRejectedOutput();
        }
    }
}
