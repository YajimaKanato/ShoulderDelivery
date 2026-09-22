using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>投擲結果のDTOを生成する役割を持つクラス</summary>
    public static class ThrowCardboardOutputService
    {
        /// <summary>
        /// 投擲失敗を生成する
        /// </summary>
        /// <returns>投擲失敗DTO</returns>
        public static ThrowCardboardRejectedOutput Rejected()
        {
            return new ThrowCardboardRejectedOutput();
        }

        /// <summary>
        /// 投擲成功を生成する
        /// </summary>
        /// <param name="id">投擲した段ボールのID</param>
        /// <returns>投擲成功DTO</returns>
        public static ThrowCardboardAcceptOutput Accepted(CardboardId id)
        {
            return new ThrowCardboardAcceptOutput(id);
        }
    }
}
