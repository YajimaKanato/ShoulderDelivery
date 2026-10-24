namespace ShoulderDelivery.UseCase
{
    /// <summary>配達失敗結果を持つDTO</summary>
    public readonly struct DeliveryFailedOutput
    {
        /// <summary>
        /// 配達失敗のDTOを生成するメソッド
        /// </summary>
        /// <returns>配達失敗のDTO</returns>
        internal static DeliveryFailedOutput Missed()
        {
            return new DeliveryFailedOutput();
        }
    }
}
